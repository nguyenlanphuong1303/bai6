using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class SachService
    {
        public List<Sach> GetAll()
        {
            using (Model1 context = new Model1())
            {
                return context.Saches.Include(s => s.LoaiSach).ToList();
            }
        }

        public List<Sach> GetSachByNamXB()
        {
            using (Model1 context = new Model1())
            {
                return context.Saches.Include(s => s.LoaiSach).OrderByDescending(s => s.NamXB).ToList();
            }
        }

        public bool AddSach(Sach s)
        {
            using (Model1 context = new Model1())
            {
                if (context.Saches.Any(x => x.MaSach == s.MaSach)) return false;
                context.Saches.Add(s);
                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateSach(Sach s)
        {
            using (Model1 context = new Model1())
            {
                var existing = context.Saches.Find(s.MaSach);
                if (existing == null) return false;
                existing.TenSach = s.TenSach;
                existing.NamXB = s.NamXB;
                existing.MaLoai = s.MaLoai;

                context.SaveChanges();
                return true;
            }
        }

        public Sach GetId(string maSach)
        {
            using (Model1 context = new Model1())
            {
                return context.Saches.Include(s => s.LoaiSach).FirstOrDefault(x => x.MaSach == maSach);
            }
        }

        public bool DeleteSach(string maSach)
        {
            using (Model1 context = new Model1())
            {
                var xoaSach = context.Saches.FirstOrDefault(x => x.MaSach == maSach);
                if (xoaSach == null) return false;

                context.Saches.Remove(xoaSach);
                context.SaveChanges();
                return true;
            }
        }
    }
}

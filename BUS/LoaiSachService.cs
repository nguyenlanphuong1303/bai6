using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiSachService
    {
        public List<LoaiSach> GetAll()
        {
            using (Model1 context = new Model1())
            {
                return context.LoaiSaches.ToList();
            }
        }
    }
}

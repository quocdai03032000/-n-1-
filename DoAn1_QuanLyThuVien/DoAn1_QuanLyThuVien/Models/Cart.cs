using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn1_QuanLyThuVien.Models
{
    public class Cart
    {
        public class CartItem
        {
            public Sach sach { get; set; }
            public string MSSV { get; set; }
            public string HinhAnh { get; set; }
        }
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem>Items
        {
            get { return Items; }
        }
        public void Add_Sach_Cart(Sach _sach)
        {
            var check = Items.FirstOrDefault(s => s.sach.id == _sach.id);
            if (check == null)
                items.Add(new CartItem
                {
                    sach = _sach,                    
                });
        }
    }
    
}
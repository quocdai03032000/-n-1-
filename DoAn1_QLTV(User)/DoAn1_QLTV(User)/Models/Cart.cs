using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn1_QLTV_User_.Models
{
    public class CartItem
    {
        public DauSach rent_Book { get; set; }
        public int numofBook { get; set; }

    }

    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void add_item(DauSach ds, int quantity = 1)
        {
            var item = items.FirstOrDefault(s => s.rent_Book.MaDauSach.Equals(ds.MaDauSach));
            if (item == null)
            {
                items.Add(new CartItem
                {
                    rent_Book = ds,
                    numofBook = quantity
                });
            }
            else
            {
                item.numofBook += quantity;
            }
        }
    }
}
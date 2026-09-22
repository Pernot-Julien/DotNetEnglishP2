using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Runtime;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();


        private List<CartLine> _persistCartLines = new List<CartLine>(); // stocke l'état du panier entre les appels

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return _persistCartLines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // TODO implement the method
            if (quantity > 0)
            {
                List<CartLine> cartLineList = GetCartLineList(); 
                CartLine newCartLine = new CartLine();
                newCartLine.Product = product;
                newCartLine.Quantity = quantity;

                bool sameProduct = false;

                foreach (CartLine cartLine in cartLineList)
                {
                    if (cartLine.Product.Id == product.Id)
                    {
                        cartLine.Quantity += quantity;
                        sameProduct = true;
                    }
                }
                if (sameProduct == false) 
                {
                    cartLineList.Add(newCartLine);
                }    
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // TODO implement the method
            List<CartLine> cartLineList = GetCartLineList();
            double totalValue = 0;
            foreach ( CartLine cartLineListItem in cartLineList)
            {
                 totalValue += cartLineListItem.Quantity * cartLineListItem.Product.Price; 
            }
            return totalValue;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // TODO implement the method
            List<CartLine> cartLineList = GetCartLineList();

            int productNumber = 0;
            foreach (CartLine cartLineListItem in cartLineList)
            {
                productNumber += cartLineListItem.Quantity;
            }
            double averagePriceValue = GetTotalValue() / productNumber;
            return averagePriceValue;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // TODO implement the method
            List<CartLine> cartLineList = GetCartLineList();
            Product findProductID = null;
            foreach (CartLine cartLineListItem in cartLineList)
            {
                if (productId == cartLineListItem.Product.Id)
                {
                    findProductID = cartLineListItem.Product;
                }
            }
            return findProductID;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

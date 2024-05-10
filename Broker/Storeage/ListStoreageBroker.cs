﻿using ClothesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.Broker.Storeage
{
    internal class ListStoreageBroker : IStoreageBroker
    {
        List<SoldProducts> demoClothes = new List<SoldProducts>();
        List<Clothes> clothes = new List<Clothes>();
        public ListStoreageBroker()
        {
            this.clothes.Add(new Clothes()
            {
                Id = 1,
                Model = "GUCCI",
                Type = ClothesType.PoloShirt,
                Cost = 100000,
                Size = 58,
                Color = "Red",
                Discraption = "Cotton, XL",
                Amount = 20
            });
            this.clothes.Add(new Clothes
            {
                Id = 2,
                Model = "LACOSTI",
                Type = ClothesType.Jeans,
                Cost = 120000,
                Size = 55,
                Color = "Blue",
                Discraption = "Cotton 50%, synthetics 50%, XXl",
                Amount = 16
            });
            this.clothes.Add(new Clothes ()
            {
                Id = 3,
                Model = "Zara",
                Type = ClothesType.Shoes,
                Cost = 200000,
                Size = 60,
                Color = "Black",
                Discraption = "Skin 100%, 60",
                Amount = 23
            });
        }

        public Clothes AddClothes(Clothes clothes)
        {
            this.clothes.Add(clothes);
            return clothes;
        }

        public List<Clothes> AddRangeClothes(List<Clothes> clothes)
        {
            this.clothes.AddRange(clothes);
            return clothes;
        }

        public bool DeleteClothes(int id)
        {
            foreach (Clothes clothesIteim in this.clothes)
            {
                if (clothesIteim.Id == id)
                {
                    this.clothes.Remove(clothesIteim);
                    return true;
                }
            }
            return false;
        }

        public List<Clothes> GetAllClothes()
        {
            throw new NotImplementedException();
        }

        public Clothes GetClothes(int id)
        {
             foreach(Clothes clothesIteim in this.clothes)
            {
                if(clothesIteim.Id == id)
                {
                    return clothesIteim;
                }
            }
            return new Clothes();
        }

        public void PurchaseClothes(string productType)
        {
            for (int itaration = 0; itaration < clothes.Count(); itaration++)
            {
                if (clothes[itaration] is not null)
                {
                    if (clothes[itaration].Type.ToString() == productType)
                    {
                        if (IsClothesAndAdded(clothes[itaration]) is true)
                        {
                            clothes[itaration].Amount -= 1;
                        }
                    }
                }
            }
        }

        private bool IsClothesAndAdded(Clothes clothes)
        {
            bool isThere = false;
            foreach (var clothesItem in demoClothes)
            {
                if (clothesItem.Equals(clothes))
                {
                    clothesItem.Amount += 1;
                    clothesItem.Balance += clothes.Cost;
                    isThere = true;
                    return true;
                }
            }

            if (isThere is false)
            {
                var info = new SoldProducts()
                {
                    Id = clothes.Id,
                    Model = clothes.Model,
                    Amount = 1,
                    Balance = clothes.Cost
                };

                this.demoClothes.Add(info);
                isThere = true;
                return isThere;
            }
            return isThere;
        }

        public List<SoldProducts> SoldInformationClothes() =>
         this.demoClothes;

        public Clothes UpdateClothes(int id, Clothes clothes)
        {
          for(int itration = 0; itration < this.clothes.Count(); itration++)
            {
                if (this.clothes[itration].Id == id)
                {
                    this.clothes[itration] = new Clothes()
                    {
                        Model = clothes.Model,
                        Type = clothes.Type,
                        Cost = clothes.Cost,
                        Size = clothes.Size,
                        Color = clothes.Color,
                        Discraption = clothes.Discraption,
                        Amount = clothes.Amount
                    };

                    return clothes;
                }
            }
            return new Clothes();

        }
    }
}

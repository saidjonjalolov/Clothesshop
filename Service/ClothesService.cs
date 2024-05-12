﻿using ClothesShop.Broker.Logging;
using ClothesShop.Broker.Storeage;
using ClothesShop.Models;


namespace ClothesShop.Service
{
    internal class ClothesService : IClothesService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStoreageBroker listStoreageBroker;

        public ClothesService()
        {
            this.listStoreageBroker = new ListStoreageBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public bool Delete(int id)
        {
            return id is 0
                ? InvalidDeleteId()
                : ValidationAndDelete(id);
        }
        public Clothes InsertClothes(Clothes clothes)
        {
            return clothes is null
                    ? InvalidInsertClothes()
                    : ValidationAndInsertClothes(clothes);
        }

        public List<Clothes> InsertRangeClothes(List<Clothes> clothes)
        {
            return clothes is null
               ? InvalidInsertRangeClothes()
               : ValidationAndInsertRangeClothes(clothes);
        }
        public void Purchase(string model)
        {
            if (model is null)
            {
                InvalidPurchase();
            }
            else
            {
                ValidationAndPurchase(model);
            }
        }

        public List<Clothes> ReadAllClothes()
        {
            var clothes = this.listStoreageBroker.GetAllClothes();
            foreach (var clothesItem in clothes)
            {
                if (clothesItem is not null)
                {
                    this.loggingBroker.LogInformation(
                        $"Id  {clothesItem.Id}\n" +
                        $"Model {clothesItem.Model}\n" +
                        $"Type {clothesItem.Type} \n" +
                        $"Cost {clothesItem.Cost} \n" +
                        $"Amount {clothesItem.Amount} \n" +
                        $"Discreption {clothesItem.Discraption} \n");
                }
            }
            return clothes;
        }

        public Clothes ReadClothes(int id)
        {
            return id is 0
               ? InvalidReadClothesId()
               : ValidationAndReadClothes(id);
        }

        public List<SoldProducts> SoldInformation()
        {
            throw new NotImplementedException();
        }

        public Clothes Update(int id, Clothes clothes)
        {
            throw new NotImplementedException();
        }

        private void ValidationAndPurchase(string model)
        {
            if (String.IsNullOrEmpty(model))
            {
                this.loggingBroker.LogError("Invalid clothes model.");
            }
            else
            {
                this.loggingBroker.LogInformation("The product was sold successfully.");
            }
        }

        private void InvalidPurchase()
        {
            this.loggingBroker.LogError("The sale did not go through.");
        }

        private Clothes ValidationAndReadClothes(int id)
        {
            var clothesInformation = this.listStoreageBroker.GetClothes(id);
            if (clothesInformation is null)
            {
                this.loggingBroker.LogError("Clothes information is not found.");
                return new Clothes();
            }
            else
            {
                this.loggingBroker.LogInformation("Successfully.");
            }
            return clothesInformation;
        }

        private Clothes InvalidReadClothesId()
        {
            this.loggingBroker.LogError("Id is invalid.");
            return new Clothes();
        }

        private List<Clothes> ValidationAndInsertRangeClothes(List<Clothes> clothes)
        {
            if (clothes is null)
            {
                this.loggingBroker.LogError("No data available.");
                return new List<Clothes>();
            }
            else
            {
                this.loggingBroker.LogInformation("The clothes were washed successfully.");
            }
            return clothes;
        }

            private List<Clothes> InvalidInsertRangeClothes()
        {
            this.loggingBroker.LogError("Clothing information was not provided.");
            return new List<Clothes>();
        }

        private bool ValidationAndDelete(int id)
        {
            bool isDelete = this.listStoreageBroker.DeleteClothes(id);
            if (isDelete is true)
            {
                this.loggingBroker.LogInformation("The information in the id has been deleted.");
                return isDelete;
            }
            else
            {
                this.loggingBroker.LogError("Id is Not Found.");
                return isDelete;
            }
        }

        private bool InvalidDeleteId()
        {
            this.loggingBroker.LogError("The id information is invalid.");
            return false;
        }

        private Clothes InvalidInsertClothes()
        {
            this.loggingBroker.LogError("Clothes info is null.");
            return new Clothes();
        }

        private Clothes ValidationAndInsertClothes(Clothes clothes)
        {
            if (clothes.Id is 0
                || String.IsNullOrWhiteSpace(clothes.Model)
                || String.IsNullOrWhiteSpace(clothes.Type.ToString()))
            {
                this.loggingBroker.LogError("Invalid clothes information.");
                return new Clothes();
            }
            else
            {
                var clothesInformation = this.listStoreageBroker.AddClothes(clothes);
                if (clothesInformation is null)
                {
                    this.loggingBroker.LogError("Not added clothes info");
                    return new Clothes();
                }
                else
                {
                    this.loggingBroker.LogInformation("Sucssesfull.");
                }
                return clothes;
            }

        }
    }
}
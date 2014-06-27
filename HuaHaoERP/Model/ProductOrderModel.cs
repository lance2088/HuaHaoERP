using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HuaHaoERP.Model
{
    class ProductOrderModel : INotifyPropertyChanged
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string orderNumber;

        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }
        private Guid customerID;

        public Guid CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }
        private string deliveryDate;

        public string DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }
        private string orderDate;

        public string OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        private List<ProductOrderDetailsModel> details;

        internal List<ProductOrderDetailsModel> Details
        {
            get { return details; }
            set { details = value; NotifyPropertyChanged("Details"); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Helpers

        /// <summary>
        /// cell内容改变事件
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    class ProductOrderDetailsModel : INotifyPropertyChanged
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private Guid orderID;

        public Guid OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Guid productID;

        public Guid ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private string productNumber;

        public string ProductNumber
        {
            get { return productNumber; }
            set { productNumber = value; }
        }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private int numberOfItems;

        public int NumberOfItems
        {
            get { return numberOfItems; }
            set { numberOfItems = value; NotifyPropertyChanged("NumberOfItems"); }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged("Quantity"); }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; NotifyPropertyChanged("Unit"); }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; NotifyPropertyChanged("Remark"); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Helpers

        /// <summary>
        /// cell内容改变事件
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    public class ProductOrderModelForDataGrid
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string orderNumber;

        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }
        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }
        private string deliveryDate;

        public string DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }
        private string orderDate;

        public string OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private string numberOfItems;

        public string NumberOfItems
        {
            get { return numberOfItems; }
            set { numberOfItems = value; }
        }
        private string quantity;

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}

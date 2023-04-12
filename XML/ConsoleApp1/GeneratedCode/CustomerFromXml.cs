// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.

namespace ConsoleApp1.GeneratedCode
{
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Customer
    {

        private string titleField;

        private string firstNameField;

        private string middleNameField;

        private string lastNameField;

        private string companyNameField;

        private string salesPersonField;

        private string emailAddressField;

        private string phoneField;

        private CustomerSalesHeaders salesHeadersField;

        private byte customerIdField;

        /// <remarks/>
        public string Title
        {
            get
            {
                return titleField;
            }
            set
            {
                titleField = value;
            }
        }

        /// <remarks/>
        public string FirstName
        {
            get
            {
                return firstNameField;
            }
            set
            {
                firstNameField = value;
            }
        }

        /// <remarks/>
        public string MiddleName
        {
            get
            {
                return middleNameField;
            }
            set
            {
                middleNameField = value;
            }
        }

        /// <remarks/>
        public string LastName
        {
            get
            {
                return lastNameField;
            }
            set
            {
                lastNameField = value;
            }
        }

        /// <remarks/>
        public string CompanyName
        {
            get
            {
                return companyNameField;
            }
            set
            {
                companyNameField = value;
            }
        }

        /// <remarks/>
        public string SalesPerson
        {
            get
            {
                return salesPersonField;
            }
            set
            {
                salesPersonField = value;
            }
        }

        /// <remarks/>
        public string EmailAddress
        {
            get
            {
                return emailAddressField;
            }
            set
            {
                emailAddressField = value;
            }
        }

        /// <remarks/>
        public string Phone
        {
            get
            {
                return phoneField;
            }
            set
            {
                phoneField = value;
            }
        }

        /// <remarks/>
        public CustomerSalesHeaders SalesHeaders
        {
            get
            {
                return salesHeadersField;
            }
            set
            {
                salesHeadersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte customerId
        {
            get
            {
                return customerIdField;
            }
            set
            {
                customerIdField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class CustomerSalesHeaders
    {

        private CustomerSalesHeadersSalesHeader salesHeaderField;

        /// <remarks/>
        public CustomerSalesHeadersSalesHeader SalesHeader
        {
            get
            {
                return salesHeaderField;
            }
            set
            {
                salesHeaderField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class CustomerSalesHeadersSalesHeader
    {

        private uint salesOrderIDField;

        private DateTime orderDateField;

        private DateTime dueDateField;

        private DateTime shipDateField;

        private byte statusField;

        private string salesOrderNumberField;

        private string purchaseOrderNumberField;

        private string accountNumberField;

        private byte customerIDField;

        private ushort shipToAddressIDField;

        private ushort billToAddressIDField;

        private string shipMethodField;

        private decimal subTotalField;

        private decimal taxAmtField;

        private decimal freightField;

        private decimal totalDueField;

        private CustomerSalesHeadersSalesHeaderOrderDetail[] orderDetailsField;

        /// <remarks/>
        public uint SalesOrderID
        {
            get
            {
                return salesOrderIDField;
            }
            set
            {
                salesOrderIDField = value;
            }
        }

        /// <remarks/>
        public DateTime OrderDate
        {
            get
            {
                return orderDateField;
            }
            set
            {
                orderDateField = value;
            }
        }

        /// <remarks/>
        public DateTime DueDate
        {
            get
            {
                return dueDateField;
            }
            set
            {
                dueDateField = value;
            }
        }

        /// <remarks/>
        public DateTime ShipDate
        {
            get
            {
                return shipDateField;
            }
            set
            {
                shipDateField = value;
            }
        }

        /// <remarks/>
        public byte Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;
            }
        }

        /// <remarks/>
        public string SalesOrderNumber
        {
            get
            {
                return salesOrderNumberField;
            }
            set
            {
                salesOrderNumberField = value;
            }
        }

        /// <remarks/>
        public string PurchaseOrderNumber
        {
            get
            {
                return purchaseOrderNumberField;
            }
            set
            {
                purchaseOrderNumberField = value;
            }
        }

        /// <remarks/>
        public string AccountNumber
        {
            get
            {
                return accountNumberField;
            }
            set
            {
                accountNumberField = value;
            }
        }

        /// <remarks/>
        public byte CustomerID
        {
            get
            {
                return customerIDField;
            }
            set
            {
                customerIDField = value;
            }
        }

        /// <remarks/>
        public ushort ShipToAddressID
        {
            get
            {
                return shipToAddressIDField;
            }
            set
            {
                shipToAddressIDField = value;
            }
        }

        /// <remarks/>
        public ushort BillToAddressID
        {
            get
            {
                return billToAddressIDField;
            }
            set
            {
                billToAddressIDField = value;
            }
        }

        /// <remarks/>
        public string ShipMethod
        {
            get
            {
                return shipMethodField;
            }
            set
            {
                shipMethodField = value;
            }
        }

        /// <remarks/>
        public decimal SubTotal
        {
            get
            {
                return subTotalField;
            }
            set
            {
                subTotalField = value;
            }
        }

        /// <remarks/>
        public decimal TaxAmt
        {
            get
            {
                return taxAmtField;
            }
            set
            {
                taxAmtField = value;
            }
        }

        /// <remarks/>
        public decimal Freight
        {
            get
            {
                return freightField;
            }
            set
            {
                freightField = value;
            }
        }

        /// <remarks/>
        public decimal TotalDue
        {
            get
            {
                return totalDueField;
            }
            set
            {
                totalDueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("OrderDetail", IsNullable = false)]
        public CustomerSalesHeadersSalesHeaderOrderDetail[] OrderDetails
        {
            get
            {
                return orderDetailsField;
            }
            set
            {
                orderDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class CustomerSalesHeadersSalesHeaderOrderDetail
    {

        private uint salesOrderIDField;

        private uint salesOrderDetailIDField;

        private byte orderQtyField;

        private ushort productIDField;

        private decimal unitPriceField;

        private decimal unitPriceDiscountField;

        private decimal lineTotalField;

        /// <remarks/>
        public uint SalesOrderID
        {
            get
            {
                return salesOrderIDField;
            }
            set
            {
                salesOrderIDField = value;
            }
        }

        /// <remarks/>
        public uint SalesOrderDetailID
        {
            get
            {
                return salesOrderDetailIDField;
            }
            set
            {
                salesOrderDetailIDField = value;
            }
        }

        /// <remarks/>
        public byte OrderQty
        {
            get
            {
                return orderQtyField;
            }
            set
            {
                orderQtyField = value;
            }
        }

        /// <remarks/>
        public ushort ProductID
        {
            get
            {
                return productIDField;
            }
            set
            {
                productIDField = value;
            }
        }

        /// <remarks/>
        public decimal UnitPrice
        {
            get
            {
                return unitPriceField;
            }
            set
            {
                unitPriceField = value;
            }
        }

        /// <remarks/>
        public decimal UnitPriceDiscount
        {
            get
            {
                return unitPriceDiscountField;
            }
            set
            {
                unitPriceDiscountField = value;
            }
        }

        /// <remarks/>
        public decimal LineTotal
        {
            get
            {
                return lineTotalField;
            }
            set
            {
                lineTotalField = value;
            }
        }
    }
}
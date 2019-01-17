namespace App.Base
{
    namespace SOLIDArchitecturePrinciple
    {
        using System;
        using System.Web;
        using System.IO;
        using System.Collections.Generic;
        using System.Collections;
        using System.Collections.Concurrent;
        using System.Linq;
        /// <summary>
        /// Summary description for ErrorLog
        /// </summary>
        namespace SOLIDArchitecturePrinciple
        {
            /// <summary>
            /// 
            /// </summary>
            public class SOLIDArchitecturePrinciple
            {
                public SOLIDArchitecturePrinciple()
                {

                    ///
                    ///SOLID Architecture Principle
                    ///
                    new SOLIDPrinciples.S_SingleResponsibilityPrinciple.SingleResponsibilityPrinciple().GetSingleResponsibilityPrinciple();
                    new SOLIDPrinciples.O_OpenClosedPrinciple.OpenClosedPrinciple().GetOpenClosedPrinciple();
                    new SOLIDPrinciples.L_LiskovSubstitutionPrinciple.LiskovSubstitutionPrinciple().GetLiskovSubstitutionPrinciple();
                    new SOLIDPrinciples.I_InterfaceSegregationPrinciple.InterfaceSegregationPrinciple().GetInterfaceSegregationPrinciple();
                    new SOLIDPrinciples.D_DependencyInversionPrinciple.DependencyInversionPrinciple().GetDependencyInversionPrinciple();

                }
            }

            //What is SOLID?
            //Understanding “S” - SRP (Single responsibility principle)
            //Understanding “O” - Open closed principle
            //Understanding “L”- LSP (Liskov substitution principle)
            //Understanding “I” - ISP (Interface Segregation principle)
            //Understanding “D” - Dependency inversion principle.
            //Revising SOLID principles

            ///SOLID Principle
            namespace SOLIDPrinciples
            {
                namespace S_SingleResponsibilityPrinciple
                {
                    public class FileLogger
                    {
                        public void Handle(string error)
                        {
                            System.IO.File.WriteAllText(@"c:\Error.txt", error);
                        }
                    }
                    public class Customer
                    {
                        private FileLogger obj = new FileLogger();
                        public virtual void Add()
                        {
                            try
                            {
                                // Database code goes here
                            }
                            catch (Exception ex)
                            {
                                obj.Handle(ex.ToString());
                            }
                        }
                    }
                    public class SingleResponsibilityPrinciple
                    {
                        public void GetSingleResponsibilityPrinciple()
                        {

                        }
                    }
                }
                namespace O_OpenClosedPrinciple
                {
                    public class FileLogger
                    {
                        public void Handle(string error)
                        {
                            System.IO.File.WriteAllText(@"c:\Error.txt", error);
                        }
                    }
                    public class Customer
                    {
                        private int _CustType;
                        FileLogger obj;
                        public int CustType
                        {
                            get { return _CustType; }
                            set { _CustType = value; }
                        }

                        public virtual double getDiscount(double TotalSales)
                        {
                            if (_CustType == 1)
                            {
                                return TotalSales - 100;
                            }
                            else
                            {
                                return TotalSales - 50;
                            }
                        }
                        public virtual void Add()
                        {
                            try
                            {
                                // Database code goes here
                            }
                            catch (Exception ex)
                            {
                                obj.Handle(ex.Message.ToString());
                            }
                        }
                    }
                    public class SilverCustomer : Customer
                    {
                        public override double getDiscount(double TotalSales)
                        {
                            return base.getDiscount(TotalSales) - 50;
                        }
                    }
                    public class GoldCustomer : SilverCustomer
                    {
                        public override double getDiscount(double TotalSales)
                        {
                            return base.getDiscount(TotalSales) - 100;
                        }
                    }
                    public class Enquiry : Customer
                    {
                        public override double getDiscount(double TotalSales)
                        {
                            return base.getDiscount(TotalSales) - 50;
                        }
                    }

                    public class OpenClosedPrinciple
                    {
                        public void GetOpenClosedPrinciple()
                        {
                            List<Customer> Customers = new List<Customer>();
                            Customers.Add(new SilverCustomer());
                            Customers.Add(new GoldCustomer());
                            Customers.Add(new Enquiry());
                            foreach (Customer o in Customers)
                            {
                                o.Add();
                            }

                        }
                    }

                }
                namespace L_LiskovSubstitutionPrinciple
                {

                    public class FileLogger
                    {
                        public void Handle(string error)
                        {
                            System.IO.File.WriteAllText(@"c:\Error.txt", error);
                        }
                    }
                    public interface IDiscount
                    {
                        double getDiscount(double TotalSales);
                    }


                    public interface IDatabase
                    {
                        void Add();
                    }
                    public class Enquiry : IDiscount
                    {
                        public double getDiscount(double TotalSales)
                        {
                            return TotalSales - 5;
                        }
                    }
                    public class Customer : IDiscount, IDatabase
                    {
                        private FileLogger obj = new FileLogger();
                        public virtual void Add()
                        {
                            try
                            {
                                // Database code goes here
                            }
                            catch (Exception ex)
                            {
                                obj.Handle(ex.Message.ToString());
                            }
                        }

                        public virtual double getDiscount(double TotalSales)
                        {
                            return TotalSales;
                        }
                    }
                    public class LiskovSubstitutionPrinciple
                    {
                        public void GetLiskovSubstitutionPrinciple()
                        {
                            //IDatabase i = new Customer(new EmailLogger());

                        }
                    }
                }
                namespace I_InterfaceSegregationPrinciple
                {
                    public class InterfaceSegregationPrinciple
                    {
                        public void GetInterfaceSegregationPrinciple()
                        {

                        }
                    }
                }
                namespace D_DependencyInversionPrinciple
                {
                    public partial class Customer
                    {
                        public string CustomerID { get; set; }
                        public string CompanyName { get; set; }
                        public string ContactName { get; set; }
                        public string ContactTitle { get; set; }
                        public string Address { get; set; }
                        public string City { get; set; }
                        public string Region { get; set; }
                        public string PostalCode { get; set; }
                        public string Country { get; set; }
                        public string Phone { get; set; }
                        public string Fax { get; set; }
                    }
                    public class CustomerViewModel
                    {
                        public string CustomerID { get; set; }
                        public string CompanyName { get; set; }
                        public string ContactName { get; set; }
                        public string Country { get; set; }
                    }
                    public class CustomerList : List<Customer>, IDisposable
                    {
                        public List<Customer> Customers { get; set; }
                        public CustomerList()
                        {
                        }
                        public void SaveChanges()
                        {
                        }
                        public void Dispose()
                        {
                            throw new NotImplementedException();
                        }
                    }
                    public interface ICustomerRepository
                    {
                        List<CustomerViewModel> SelectAll();
                        CustomerViewModel SelectByID(string id);
                        void Insert(CustomerViewModel obj);
                        void Update(CustomerViewModel obj);
                        void Delete(CustomerViewModel obj);
                    }
                    public class CustomerRepository : ICustomerRepository
                    {
                        public List<CustomerViewModel> SelectAll()
                        {
                            using (CustomerList db = new CustomerList())
                            {
                                var query = from c in db.Customers
                                            orderby c.CustomerID ascending
                                            select new CustomerViewModel()
                                            {
                                                CustomerID = c.CustomerID,
                                                CompanyName = c.CompanyName,
                                                ContactName = c.ContactName,
                                                Country = c.Country
                                            };
                                return query.ToList();
                            }
                        }

                        public CustomerViewModel SelectByID(string id)
                        {
                            using (CustomerList db = new CustomerList())
                            {
                                var query = from c in db.Customers
                                            where c.CustomerID == id
                                            select new CustomerViewModel()
                                            {
                                                CustomerID = c.CustomerID,
                                                CompanyName = c.CompanyName,
                                                ContactName = c.ContactName,
                                                Country = c.Country
                                            };
                                return query.SingleOrDefault();
                            }
                        }

                        public void Insert(CustomerViewModel obj)
                        {
                            Customer newCustomer = new Customer();
                            newCustomer.CustomerID = obj.CustomerID;
                            newCustomer.CompanyName = obj.CompanyName;
                            newCustomer.ContactName = obj.ContactName;
                            newCustomer.Country = obj.Country;
                            using (CustomerList db = new CustomerList())
                            {
                                db.Customers.Add(newCustomer);
                                db.SaveChanges();
                            }
                        }

                        public void Update(CustomerViewModel obj)
                        {
                            using (CustomerList db = new CustomerList())
                            {
                                Customer existingCustomer = db.Customers.Find(x=>x.CustomerID.Equals(obj.CustomerID));
                                existingCustomer.CompanyName = obj.CompanyName;
                                existingCustomer.ContactName = obj.ContactName;
                                existingCustomer.Country = obj.Country;
                                db.SaveChanges();
                            }
                        }

                        public void Delete(CustomerViewModel obj)
                        {
                            using (CustomerList db = new CustomerList())
                            {
                                Customer existingCustomer = db.Customers.Find(x => x.CustomerID.Equals(obj.CustomerID));
                                db.Customers.Remove(existingCustomer);
                                db.SaveChanges();
                            }
                        }
                    }

                    public class DependencyInversionPrinciple
                    {
                        ICustomerRepository repository = null;
                        public DependencyInversionPrinciple()
                        { }
                        public DependencyInversionPrinciple(ICustomerRepository repository)
                        {
                            this.repository = repository;
                        }
                        public void GetDependencyInversionPrinciple()
                        {
                        }
                        public List<CustomerViewModel> Index()
                        {
                            List<CustomerViewModel> data = repository.SelectAll();
                            return data;
                        }
                    }



                    public interface ILogger
                    {
                        void Handle(string error);
                    }
                    public class FileLogger : ILogger
                    {
                        public void Handle(string error)
                        {
                            System.IO.File.WriteAllText(@"c:\Error.txt", error);
                        }
                    }
                    public class EverViewerLogger : ILogger
                    {
                        public void Handle(string error)
                        {
                            // log errors to event viewer
                        }
                    }
                    public class EmailLogger : ILogger
                    {
                        public void Handle(string error)
                        {
                            // send errors in email
                        }
                    }
                    public interface IDiscount
                    {
                        double getDiscount(double TotalSales);
                    }
                    public interface IDatabase
                    {
                        void Add();
                    }
                    public class CustomerData : IDiscount, IDatabase
                    {
                        private ILogger obj;
                        public virtual void Add(int Exhandle)
                        {
                            try
                            {
                                // Database code goes here
                            }
                            catch (Exception ex)
                            {
                                if (Exhandle == 1)
                                {
                                    obj = new FileLogger();
                                }
                                else
                                {
                                    obj = new EmailLogger();
                                }
                                obj.Handle(ex.Message.ToString());
                            }
                        }
                        public void Add()
                        {

                        }
                        public virtual double getDiscount(double TotalSales)
                        {
                            return TotalSales;
                        }
                    }
                }
            }
        }

        namespace StructData
        {
            public struct Complex
            {
                float real;
                float imaginary;

                public Complex(float real, float imaginary)
                {
                    this.real = real;
                    this.imaginary = imaginary;
                }

                public float Real
                {
                    get
                    {
                        return (real);
                    }
                    set
                    {
                        real = value;
                    }
                }

                public float Imaginary
                {
                    get
                    {
                        return (imaginary);
                    }
                    set
                    {
                        imaginary = value;
                    }
                }

                public override string ToString()
                {
                    return (String.Format("({0}, {1}i)", real, imaginary));
                }

                public static bool operator ==(Complex c1, Complex c2)
                {
                    if ((c1.real == c2.real) &&
                    (c1.imaginary == c2.imaginary))
                        return (true);
                    else
                        return (false);
                }

                public static bool operator !=(Complex c1, Complex c2)
                {
                    return (!(c1 == c2));
                }

                public override bool Equals(object o2)
                {
                    Complex c2 = (Complex)o2;

                    return (this == c2);
                }

                public override int GetHashCode()
                {
                    return (real.GetHashCode() ^ imaginary.GetHashCode());
                }

                public static Complex operator +(Complex c1, Complex c2)
                {
                    return (new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary));
                }

                public static Complex operator -(Complex c1, Complex c2)
                {
                    return (new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary));
                }

                // product of two complex numbers
                public static Complex operator *(Complex c1, Complex c2)
                {
                    return (new Complex(c1.real * c2.real - c1.imaginary * c2.imaginary,
                    c1.real * c2.imaginary + c2.real * c1.imaginary));
                }

                // quotient of two complex numbers
                public static Complex operator /(Complex c1, Complex c2)
                {
                    if ((c2.real == 0.0f) &&
                    (c2.imaginary == 0.0f))
                        throw new DivideByZeroException("Can't divide by zero Complex number");

                    float newReal =
                    (c1.real * c2.real + c1.imaginary * c2.imaginary) /
                    (c2.real * c2.real + c2.imaginary * c2.imaginary);
                    float newImaginary =
                    (c2.real * c1.imaginary - c1.real * c2.imaginary) /
                    (c2.real * c2.real + c2.imaginary * c2.imaginary);

                    return (new Complex(newReal, newImaginary));
                }

                // non-operator versions for other languages
                public static Complex Add(Complex c1, Complex c2)
                {
                    return (c1 + c2);
                }

                public static Complex Subtract(Complex c1, Complex c2)
                {
                    return (c1 - c2);
                }

                public static Complex Multiply(Complex c1, Complex c2)
                {
                    return (c1 * c2);
                }

                public static Complex Divide(Complex c1, Complex c2)
                {
                    return (c1 / c2);
                }
            }

            public class AComplexNumberClass
            {
                public static void Main()
                {
                    Complex c1 = new Complex(3, 1);
                    Complex c2 = new Complex(1, 2);

                    Console.WriteLine("c1 == c2: {0}", c1 == c2);
                    Console.WriteLine("c1 != c2: {0}", c1 != c2);
                    Console.WriteLine("c1 + c2 = {0}", c1 + c2);
                    Console.WriteLine("c1 - c2 = {0}", c1 - c2);
                    Console.WriteLine("c1 * c2 = {0}", c1 * c2);
                    Console.WriteLine("c1 / c2 = {0}", c1 / c2);
                }
            }
        }
    }
}

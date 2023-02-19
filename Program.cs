using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

namespace ProjectCRM
{

/*start of data context*/
    public class CRMSDbContext : DbContext
    {
        string mysqlString = "server=localhost;database=ProjectCRMSMasterDb;user=root;password=passw0rd";
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlServer(@"Server=.\SQLExpress; Database=CRMSMasterDb; User Id=SA; Password=password; Trusted_Connection=False; MultipleActiveResultSets=true");
           // options.UseMySql("server=localhost;database=ProjectCRMSMasterDb;user=root;password=password");
            options.UseMySql(mysqlString, ServerVersion.AutoDetect(mysqlString));
        }
        public DbSet<User> Users {get;set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }

    }
    /*end of data context*/

    /*start of models*/
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Sale> Sales { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPopular { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }

    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateApproved { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }        
    }
    public class Sale
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string TallyNo { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime DateTransacted { get; set; }
        public string ShortDateTransacted { get; set; }        
        public int Qty { get; set; }
        public double Amount { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; }
    }
    public class Vendor
    {
        public int Id { get; set; }
        public string VendorCode { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
    }

    public class Employee
    {
        public int Id {get;set;}
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public DateTime DateHired { get; set; }
        public string ImageUrl { get; set; }
    }
    public class Attendance
    {
        public int Id { get; set; }
        public string DateAttended { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Remarks { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
    /*end of models*/

    /*start of program*/
    class Program
    {
        static void Main(string[] args)
        {
            GetMenu();
        }
        static void GetMenu()
        {
            Console.Clear();
            Console.WriteLine("Project CRMS v 0");
            Console.WriteLine("[0] PRODUCTS");
            Console.WriteLine("[1] CUSTOMERS");
            Console.WriteLine("[3] VENDORS");
            Console.WriteLine("[4] CATEGORIES");
            Console.WriteLine("[5] ATTENDANCES");
            Console.WriteLine("[6] EMPLOYEES");    
            Console.WriteLine("[7] SALES");        
            Console.WriteLine("[8] Exit");
            Console.Write("Enter Number:");
            int chooseNumber = Convert.ToInt16(Console.ReadLine());
            //char rk = chooseNumber.C();

            if (chooseNumber == 0)
            {
                GetProducts();
            }
            else if (chooseNumber == 1)
            {
                GetCustomers();
            } 
            else if (chooseNumber == 3)
            {
                GetVendors();
            }
            else if (chooseNumber == 4)
            {
                GetCategories();
            }
            else if (chooseNumber==5)
            {
                //GetAttendances();
            }
            else if (chooseNumber==6)
            {
                GetEmployees();
            }
            else if (chooseNumber==7)
            {
                GetSalesReports();
            }            
            else if (chooseNumber==8)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.ReadLine();
                GetMenu();
            }
        }
        /*start of Sales*/
        static void GetSalesReports()
        {
            int Id;
            string ddate;
            Console.Clear();
            Console.WriteLine("SALES REPORTS MANAGEMENT");
            Console.WriteLine("[0] ADD SALES PER PRODUCT");
            Console.WriteLine("[1] VIEW DAILY SALES REPORT");  
            Console.WriteLine("[2] VIEW DAILY SALES SALES PER VENDOR");                      
            Console.WriteLine("[3] VIEW DAILY SALES SALES PER INVOICE");            
            Console.WriteLine("[4] VIEW DAILY SALES SALES PER TALLY");            
            Console.WriteLine("[5] VIEW DAILY SALES SALES PER PRODUCT");              
            Console.WriteLine("[6] UPDATE SALES");  
            Console.WriteLine("[7] DELETE SALES");  
            Console.WriteLine("[8] Back");  
            Console.Write("Enter a number:");
            int num = Convert.ToInt16(Console.ReadLine());    

            switch (num)
                {
                    case 0:
                    AddSales();
                    break;

                    case 1:
                    Console.Write("Date [mm/d/yyyy]:");
                    ddate = Console.ReadLine();
                    ViewDailySalesReport(ddate);
                    break;

                    case 2:
                    Console.Write("Vendor Code:");
                    string vendorCode = Console.ReadLine();
                    Console.Write("Date [mm/d/yyyy]:");
                    ddate = Console.ReadLine();
                    //ViewSalesByVendor(vendorCode, ddate);
                    break;

                    case 3:
                    Console.Write("Invoice No:");
                    string invoiceNo = Console.ReadLine();
                    Console.Write("Date [mm/d/yyyy]:");
                    ddate = Console.ReadLine();
                    ViewSalesByInvoice(invoiceNo, ddate);
                    break; 

                    case 4:
                    Console.Write("Tally No:");
                    string tallyNo = Console.ReadLine();
                    ViewSalesByTally(tallyNo);
                    break;                      

                    case 5:
                    Console.Write("Product Code:");
                    string productCode = Console.ReadLine();
                    Console.Write("Date [mm/d/yyyy]:");
                    ddate = Console.ReadLine();
                    ViewSalesByProduct(productCode,ddate);
                    break;     

                    case 6:
                    Console.Write("Id:");
                    Id = Convert.ToInt16(Console.ReadLine());
                    UpdateSale(Id);
                    break;     

                    case 7:
                    Console.Write("Id:");
                    Id = Convert.ToInt16(Console.ReadLine());
                    DeleteSale(Id);
                    break;   

                    case 8:
                    GetMenu();
                    break;  

                    default:
                    GetMenu();
                    break;
                }                                
        }
        static void AddSales()
        {
            Console.Clear();
            Console.WriteLine("ADD SALES");
            Console.Write("Invoice No.: ");
            string vCode = Console.ReadLine();
            Console.Write("Tally No.[mmddyyvcode]: ");
            string tallyNo = Console.ReadLine();   
            Console.Write("Product ID: ");
            int productId = Convert.ToInt16(Console.ReadLine());                        
            Console.Write("Customer ID: ");
            int custId = Convert.ToInt16(Console.ReadLine());     
            Console.Write("Quantity: ");
            int qty = Convert.ToInt16(Console.ReadLine());    
            Console.Write("Amount: ");
            double amt = Convert.ToDouble(Console.ReadLine());       
            Console.WriteLine("Do you wish to Save? Y to save N to cancel.");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                using (var dbContext = new CRMSDbContext())
                {
                    var sale = new Sale
                    {
                        InvoiceNo = vCode,
                        TallyNo = tallyNo,
                        ProductId = productId,
                        CustomerId = custId,
                        DateTransacted =DateTime.Now,
                        ShortDateTransacted = DateTime.Now.ToShortDateString(),
                        Qty = qty,
                        Amount = amt,
                    };
                    dbContext.Add(sale);
                    dbContext.SaveChanges();
                    Console.WriteLine("Sales Added.");
                    Console.ReadLine();
                    GetSalesReports();
                }
            }
            else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Cancelled.");
                Console.ReadLine();
                GetSalesReports();
            }
            else
            {
                Console.WriteLine("Something went wrong.");
                Console.ReadLine();
                GetSalesReports();
            }
        }
        static void ViewDailySalesReport(string toDate)
        {
            double totalAmount;
            Console.Clear();
            Console.WriteLine("VIEW DAILY SALES REPORT");
            Console.WriteLine("Date: "+toDate);
            Console.WriteLine("Tally No.\t"+"Invoice No.\t"+"Product Code\t"+"Product Name\t"+"Quantity\t"+"Amount\t"+"Total Amt\t"+"Purchased by");
            using (var dbContext = new CRMSDbContext())
            {
                var sales = from s in dbContext.Sales.Where(s => s.ShortDateTransacted == toDate)
                            join p in dbContext.Products on s.ProductId equals p.Id
                            join c in dbContext.Customers on s.CustomerId equals c.Id
                           // join v in dbContext.Vendors on s.VendorId equals v.Id
                            select new
                            {
                                TallyNo = s.TallyNo,
                                InvoiceNo = s.InvoiceNo,
                                ProductCode = p.ProductCode,
                                ProductName = p.ProductDescription,
                                Quantity = s.Qty,
                                Amount = s.Amount,
                                CustomerName = c.Name
                            };
                            var salesTotal = from sale in sales
                                            group sale by sale.ProductCode into saleGrp
                                            select new 
                                            {
                                                ProductCode = saleGrp.Key,
                                                TotalQty = saleGrp.Sum(x => x.Quantity),
                                                TotalAmount = saleGrp.Sum(x => x.Amount)
                                            };
                foreach (var sale in salesTotal)
                {
                    double qty = Convert.ToDouble(sale.TotalQty);
                    double amt = sale.TotalAmount;
                    totalAmount = qty*amt;
                    Console.WriteLine(sale.ProductCode+"\t"+sale.TotalQty+"\t"+sale.TotalAmount+"\t"+totalAmount);
                } 
            }
            Console.WriteLine("Do you wish to add Sales? Y to yes N to Cancel." );
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                AddSales();
            }
            else if (choice == 'N' || choice == 'n')
            {
                GetSalesReports();
            }
            else
            {
                Console.WriteLine("Press any key to continue?");
                Console.ReadLine();
            }
        }
        static void ViewSalesByVendor(string vendorCode, string dDate)
        {
            double totalAmount;
            Console.Clear();
            Console.WriteLine("VIEW DAILY SALES REPORT");
            Console.WriteLine("Vendor Code: "+vendorCode+"\tDate: "+dDate);
            Console.WriteLine("Tally No.\t"+"Invoice No.\t"+"Product Code\t"+"Product Name\t"+"Quantity\t"+"Amount\t"+"Total Amt\t"+"Purchased by");
            using (var dbContext = new CRMSDbContext())
            {
                var sales = from s in dbContext.Sales.Where(s => s.ShortDateTransacted == dDate)
                            join p in dbContext.Products on s.ProductId equals p.Id
                            join c in dbContext.Customers on s.CustomerId equals c.Id
                           // join v in dbContext.Vendors on s.VendorId equals v.Id
                            select new
                            {
                                TallyNo = s.TallyNo,
                                InvoiceNo = s.InvoiceNo,
                                ProductCode = p.ProductCode,
                                ProductName = p.ProductDescription,
                                Quantity = s.Qty,
                                Amount = s.Amount,
                                CustomerName = c.Name
                            };
                foreach (var sale in sales)
                {
                    double qty = Convert.ToDouble(sale.Quantity);
                    double amt = sale.Amount;
                    totalAmount = qty*amt;
                    Console.WriteLine(sale.TallyNo+"\t"+sale.InvoiceNo+"\t"+sale.ProductCode+"\t"+sale.ProductName+"\t"+sale.Quantity+"\t"+sale.Amount+"\t"+totalAmount+"\t"+sale.CustomerName);
                } 
            }
            Console.WriteLine("Do you wish to add Sales? Y to yes N to Cancel." );
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                AddSales();
            }
            else if (choice == 'N' || choice == 'n')
            {
                GetSalesReports();
            }
            else
            {
                Console.WriteLine("Press any key to continue?");
                Console.ReadLine();
            }
        } 
       static void ViewSalesByInvoice(string invoiceNo, string dDate)
        {
            double totalAmount;
            Console.Clear();
            Console.WriteLine("VIEW DAILY SALES REPORT");
            Console.WriteLine("Invoice: "+invoiceNo+"\tDate: "+dDate);
            Console.WriteLine("Date\t"+"Product Code\t"+"Product Name\t"+"Quantity\t"+"Amount\t"+"Total Amt\t"+"Purchased by");
            using (var dbContext = new CRMSDbContext())
            {
                var sales = from s in dbContext.Sales.Where(s => s.InvoiceNo == invoiceNo && s.ShortDateTransacted == dDate)
                            join p in dbContext.Products on s.ProductId equals p.Id
                            join c in dbContext.Customers on s.CustomerId equals c.Id
                           // join v in dbContext.Vendors on s.VendorId equals v.Id
                            select new
                            {
                                Date = s.ShortDateTransacted,
                                ProductCode = p.ProductCode,
                                ProductName = p.ProductDescription,
                                Quantity = s.Qty,
                                Amount = s.Amount,
                                CustomerName = c.Name
                            };
                foreach (var sale in sales)
                {
                    double qty = Convert.ToDouble(sale.Quantity);
                    double amt = sale.Amount;
                    totalAmount = qty*amt;
                    Console.WriteLine(sale.Date+"\t"+sale.ProductCode+"\t"+sale.ProductName+"\t"+sale.Quantity+"\t"+sale.Amount+"\t"+totalAmount+"\t"+sale.CustomerName);
                } 
            }
            Console.WriteLine("Do you wish to add Sales? Y to yes N to Cancel." );
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                AddSales();
            }
            else if (choice == 'N' || choice == 'n')
            {
                GetSalesReports();
            }
            else
            {
                Console.WriteLine("Press any key to continue?");
                Console.ReadLine();
            }
        }         
       static void ViewSalesByTally(string tallyNo)
        {
            double totalAmount;
            Console.Clear();
            Console.WriteLine("VIEW DAILY SALES REPORT");
            Console.WriteLine("Date: "+tallyNo);
            Console.WriteLine("Date\t"+"Product Code\t"+"Product Name\t"+"Quantity\t"+"Amount\t"+"Total Amt");
            using (var dbContext = new CRMSDbContext())
            {
                var sales = from s in dbContext.Sales.Where(s => s.TallyNo == tallyNo)
                            join p in dbContext.Products on s.ProductId equals p.Id
                            join c in dbContext.Customers on s.CustomerId equals c.Id
                           // join v in dbContext.Vendors on s.VendorId equals v.Id
                            select new
                            {
                                ProductCode = p.ProductCode,
                                ProductName = p.ProductDescription,
                                Quantity = s.Qty,
                                Amount = s.Amount,
                            };
                foreach (var sale in sales)
                {
                    double qty = Convert.ToDouble(sale.Quantity);
                    double amt = sale.Amount;
                    totalAmount = qty*amt;
                    Console.WriteLine(sale.ProductCode+"\t"+sale.ProductName+"\t"+sale.Quantity+"\t"+sale.Amount+"\t"+totalAmount);
                } 
            }
            Console.WriteLine("Do you wish to add Sales? Y to yes N to Cancel." );
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                AddSales();
            }
            else if (choice == 'N' || choice == 'n')
            {
                GetSalesReports();
            }
            else
            {
                Console.WriteLine("Press any key to continue?");
                Console.ReadLine();
            }
        }    
       static void ViewSalesByProduct(string productCode, string dDate)
        {
            double totalAmount;
            Console.Clear();
            Console.WriteLine("VIEW DAILY SALES REPORT");
            Console.WriteLine("Product Code: "+productCode+"\tDate: "+dDate);
            Console.WriteLine("Tally No\t"+"Invoice No\t"+"Quantity\t"+"Amount\t"+"Total Amt\t"+"Purchased by");
            using (var dbContext = new CRMSDbContext())
            {
                var sales = from s in dbContext.Sales.Include(s =>s.Product).Where(s => s.Product.ProductCode == productCode && s.ShortDateTransacted == dDate)
                            join p in dbContext.Products on s.ProductId equals p.Id
                            join c in dbContext.Customers on s.CustomerId equals c.Id
                           // join v in dbContext.Vendors on s.VendorId equals v.Id
                            select new
                            {
                                TallyNo = s.TallyNo,
                                InvoiceNo = s.InvoiceNo,
                                Quantity = s.Qty,
                                Amount = s.Amount,
                                CustomerName = c.Name
                            };
                foreach (var sale in sales)
                {
                    double qty = Convert.ToDouble(sale.Quantity);
                    double amt = sale.Amount;
                    totalAmount = qty*amt;
                    Console.WriteLine(sale.TallyNo+"\t"+sale.InvoiceNo+"\t"+sale.Quantity+"\t"+sale.Amount+"\t"+totalAmount+"\t"+sale.CustomerName);
                } 
            }
            Console.WriteLine("Do you wish to add Sales? Y to yes N to Cancel." );
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                AddSales();
            }
            else if (choice == 'N' || choice == 'n')
            {
                GetSalesReports();
            }
            else
            {
                Console.WriteLine("Press any key to continue?");
                Console.ReadLine();
            }
        } 
        static void UpdateSale(int saleId)
        {
            Console.Clear();
            Console.WriteLine("UPDATE SALES");
            using (var dbContext = new CRMSDbContext())
            {
                var sale = dbContext.Sales.Include(c => c.Customer).SingleOrDefault(s =>s.Id == saleId);
                if (sale != null)
                {
                    Console.WriteLine("Name: "+sale.TallyNo+"\n"+"Address: "+sale.InvoiceNo+"Address: "+sale.Customer.Name);                     
                    Console.WriteLine("Update:");
                    Console.Write("Invoice No.: ");
                    string invoiceNo = Console.ReadLine();
                    Console.Write("Tally No.[mmddyyvcode]: ");
                    string tallyNo = Console.ReadLine();   
                    Console.Write("Product ID: ");
                    int productId = Convert.ToInt16(Console.ReadLine());                        
                    Console.Write("Customer ID: ");
                    int custId = Convert.ToInt16(Console.ReadLine());     
                    Console.Write("Quantity: ");
                    int qty = Convert.ToInt16(Console.ReadLine());    
                    Console.Write("Amount: ");
                    double amt = Convert.ToDouble(Console.ReadLine());      

                    Console.WriteLine("Do you want to Save? Y to Save, N to Cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        sale.InvoiceNo = invoiceNo;
                        sale.TallyNo = tallyNo;
                        sale.ProductId = productId;
                        sale.CustomerId = custId;
                        sale.Qty = qty;
                        sale.Amount = amt;

                        dbContext.SaveChanges(); 
                        Console.WriteLine("Sale Updated.");                        
                        Console.ReadLine();
                        GetSalesReports();     
                    } 
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.WriteLine("Cancelled.");                        
                        Console.ReadLine();
                        GetSalesReports();                              
                    } 
                    else
                    {
                        Console.WriteLine("Something went wrong.");                        
                        Console.ReadLine();
                        GetSalesReports();    
                    }                                       
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetSalesReports(); 
                }
            }
        }     
        static void DeleteSale(int saleId)
        {
            Console.Clear();
            Console.WriteLine("DELETE A SALE");
            using (var dbContext = new CRMSDbContext())
            {
                var sale = dbContext.Sales.Find(saleId);
                if (sale != null)
                {
                    Console.WriteLine("Delete? Y to Save, N to Cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        dbContext.Remove(sale); 
                        Console.WriteLine("Sale Deleted.");                        
                        Console.ReadLine();
                        GetSalesReports();     
                    } 
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.WriteLine("Cancelled.");                        
                        Console.ReadLine();
                        GetSalesReports();                              
                    } 
                    else
                    {
                        Console.WriteLine("Something went wrong.");                        
                        Console.ReadLine();
                        GetSalesReports();    
                    }                                       
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetSalesReports(); 
                }
            }
        }                              
        /*end of sales*/
        /*start of employees*/
        static void GetEmployees()
        {
            int Id;
            Console.Clear();
            Console.WriteLine("EMPLOYEE MANAGEMENT");
            Console.WriteLine("[0] ADD EMPLOYEE");
            Console.WriteLine("[1] VIEW EMPLOYEES");              
            Console.WriteLine("[2] VIEW EMPLOYEE by ID");
            Console.WriteLine("[3] UPDATE EMPLOYEE");            
            Console.WriteLine("[4] DELETE EMPLOYEE");                      
            Console.WriteLine("[5] back");
            Console.Write("Enter a number:");
            int num = Convert.ToInt16(Console.ReadLine());

            switch (num)
                {
                    case 0:
                    AddEmployee();
                    break;

                    case 1:
                    ViewEmployees();
                    break;

                    case 2:
                    Console.Write("Employee Id:");
                    Id = Convert.ToInt16(Console.ReadLine());
                    ViewEmployeeById(Id);
                    break;

                    case 3:
                    Console.Write("Employee Id:");
                    Id = Convert.ToInt16(Console.ReadLine());
                    UpdateEmployee(Id);
                    break; 

                    case 4:
                    Console.Write("Employee Id:");
                    Id = Convert.ToInt16(Console.ReadLine());
                    DeleteEmployee(Id);
                    break;                      

                    case 5:
                    GetMenu();
                    break;                                               

                    default:
                    GetMenu();
                    break;
                }  
       
        }    

        static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("ADD A EMPLOYEE");
            Console.Write("Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("Full Address: ");
            string fullAddress = Console.ReadLine();
            Console.Write("Do you wish to save? Y to save N to cancel: ");            
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                using (var dbContext = new CRMSDbContext())
                {
                    var employee = new Employee
                    {
                        FullName = fullName,
                        FullAddress =fullAddress,
                        DateHired = DateTime.Now,
                        ImageUrl = "nophoto.png"
                    };
                    dbContext.Add(employee);
                    dbContext.SaveChanges();
                    Console.WriteLine("Employee Added.");
                    Console.ReadLine();
                    GetEmployees();
                }                
            }
            else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Cancelled.");
                Console.ReadLine();
                GetEmployees();
            }
            else
            {
                Console.WriteLine("Something went wrong. Try Again");
                Console.ReadLine();
                GetEmployees(); 
            }
        }   
        static void ViewEmployees()
        {
            Console.Clear();
            Console.WriteLine("VIEW ALL EMPLOYEES");
            using (var dbContext = new CRMSDbContext())
            {
                Console.WriteLine("Id"+"\t"+"Full Name"+"\t"+"Address");
                var employees = dbContext.Employees;
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.Id+"\t"+employee.FullName+"\t"+employee.FullAddress);
                }
            }
            Console.WriteLine("Add Employee? Y to yes, N to cancel.");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                AddEmployee();
            }
            else if (choice == 'N' || choice == 'n')
            {
                GetEmployees();
            }
            else
            {
                Console.WriteLine("Something went wrong. Try Again");
                Console.ReadLine();
                GetEmployees(); 
            }
        }   
        static void ViewEmployeeById(int Id)
        {
            Console.Clear();
            Console.WriteLine("EMPLOYEE INFORMATION");
            using (var dbContext = new CRMSDbContext())
            {
                var employee = dbContext.Employees.Find(Id);
                if (employee != null)
                {
                    Console.WriteLine("Name: "+employee.FullName+"\n"+"Address: "+employee.FullAddress);             
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetEmployees(); 
                }


            }
            Console.WriteLine("Update? Y to yes, N to cancel.");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                UpdateEmployee(Id);
            }
            else if (choice == 'N' || choice == 'n')
            {
                GetEmployees();
            }
            else
            {
                Console.WriteLine("Something went wrong. Try Again");
                Console.ReadLine();
                GetEmployees(); 
            }
        }     
        static void UpdateEmployee(int Id)
        {
            Console.Clear();
            Console.WriteLine("UPDATE EMPLOYEE INFORMATION");
            using (var dbContext = new CRMSDbContext())
            {
                var employee = dbContext.Employees.Find(Id);
                if (employee != null)
                {
                    Console.WriteLine("Name: "+employee.FullName+"\n"+"Address: "+employee.FullAddress);                     
                    Console.WriteLine("Update:");
                    Console.Write("Name: ");                                     
                    string name = Console.ReadLine();      
                    Console.Write("Address: ");                                     
                    string address = Console.ReadLine();     

                    Console.WriteLine("Do you want to Save? Y to Save, N to Cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        employee.FullName = name;
                        employee.FullAddress = address;
                        dbContext.SaveChanges(); 
                        Console.WriteLine("Employee Updated.");                        
                        Console.ReadLine();
                        GetEmployees();     
                    } 
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.WriteLine("Cancelled.");                        
                        Console.ReadLine();
                        GetEmployees();                              
                    } 
                    else
                    {
                        Console.WriteLine("Something went wrong.");                        
                        Console.ReadLine();
                        GetEmployees();    
                    }                                       
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetEmployees(); 
                }
            }
        }    
     static void DeleteEmployee(int Id)
        {
            Console.Clear();
            Console.WriteLine("DELETE AN EMPLOYEE");
            using (var dbContext = new CRMSDbContext())
            {
                var employee = dbContext.Employees.Find(Id);
                if (employee != null)
                {
                    Console.WriteLine("Name: "+employee.FullName+"\n"+"Address: "+employee.FullAddress);                        

                    Console.WriteLine("Do you want to DELETE? Y to Delete, N to Cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        dbContext.Remove(employee); 
                        dbContext.SaveChanges();
                        Console.WriteLine("Employee Deleted.");                        
                        Console.ReadLine();
                        GetEmployees();     
                    } 
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.WriteLine("Cancelled.");                        
                        Console.ReadLine();
                        GetEmployees();                              
                    } 
                    else
                    {
                        Console.WriteLine("Something went wrong.");                        
                        Console.ReadLine();
                        GetEmployees();    
                    }                                       
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetEmployees(); 
                }
            }
        }                               
        /*end of employees*/

        /*start of vendors*/
        static void GetVendors()
        {
            Console.Clear();
            Console.WriteLine("VENDOR MANAGEMENT");
            Console.WriteLine("[0] ADD VENDOR");
            Console.WriteLine("[1] VIEW VENDORS");              
            Console.WriteLine("[2] VIEW VENDOR");         
            Console.WriteLine("[3] DELETE VENDOR");                      
            Console.WriteLine("[4] back");
            Console.Write("Enter a number:");
            int num = Convert.ToInt16(Console.ReadLine());
            switch (num)
            {
                case 0:
                AddVendor();
                break;

                case 1:
                ViewVendors();
                break;

                case 2:
                ViewVendorByCode();
                break;

                case 3:
                DeleteVendor();
                break;   

                case 4:
                GetMenu();
                break;                                               

                default:
                GetMenu();
                break;
            }            
        }
        static void AddVendor()
        {
            Console.Clear();
            Console.WriteLine("ADD A VENDOR");
            Console.Write("Vendor Code: ");
            string vendorCode = Console.ReadLine();
            Console.Write("Employee ID: ");
            int empId = Convert.ToInt16(Console.ReadLine());
            Console.Write("Do you wish to save? Y to save N to cancel: ");            
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                using (var dbContext = new CRMSDbContext())
                {
                    var vendor = new Vendor
                    {
                        VendorCode = vendorCode,
                        EmployeeId=  empId
                    };
                    dbContext.Add(vendor);
                    dbContext.SaveChanges();
                    Console.WriteLine("Vendor Added.");
                    Console.ReadLine();
                    GetVendors();
                }                
            }
            else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Cancelled.");
                Console.ReadLine();
                GetVendors();
            }
            else
            {
                Console.WriteLine("Something went wrong. Try Again");
                Console.ReadLine();
                GetVendors(); 
            }
        }
        static void ViewVendors()
        {
            Console.Clear();
            Console.WriteLine("VIEW ALL VENDORS");
            using (var dbContext = new CRMSDbContext())
            {
                var vendors = from v in dbContext.Vendors
                                join e in dbContext.Employees on v.EmployeeId equals e.Id
                                select new
                                {
                                    VendorCode = v.VendorCode,
                                    VendorName = e.FullName,
                                };
                Console.WriteLine("Vendor Code"+"\t"+"Vendor Name");
                foreach (var vendor in vendors)
                {
                    Console.WriteLine(vendor.VendorCode+"\t"+vendor.VendorName);
                }
            }
            Console.Write("Do you wish to Add? Y to proceed, N to exit");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                AddVendor();
            }
            else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Press any to continue.");
                Console.ReadLine();
                GetVendors();
            }
            else
            {
                Console.WriteLine("Something went wrong.");
                Console.ReadLine();
                GetVendors();
            }
        }
        static void ViewVendorByCode()
        {
            Console.Clear();
            Console.WriteLine("VIEW VENDOR BY CODE");
            Console.Write("Vendor Code: ");
            string vendorCode = Console.ReadLine();

            using (var dbContext = new CRMSDbContext())
            {
                var vendor = dbContext.Vendors.Include(v => v.Employee).SingleOrDefault(v => v.VendorCode == vendorCode);
                if (vendor != null)
                {
                    Console.WriteLine("Vendor Name: "+vendor.Employee.FullName);
                }
            }
            Console.WriteLine("Do you wish to Delete? [Y] to Delete, [N] to Exit.");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                DeleteVendor();
            }
             else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Press any to continue.");
                Console.ReadLine();
                GetVendors();
            }
            else
            {
                Console.WriteLine("Something went wrong.");
                Console.ReadLine();
                GetVendors();
            }
        }

        static void DeleteVendor()
        {
            Console.Clear();
            Console.WriteLine("DELETE A VENDOR");
            Console.Write("Vendor Code: ");
            string vendorCode = Console.ReadLine();
            using (var dbContext = new CRMSDbContext())
            {
                var vendor = dbContext.Vendors.SingleOrDefault(v => v.VendorCode == vendorCode);
                if (vendor != null)
                {
                    Console.Write("Delete? Y to Save N to Cancel");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        dbContext.Remove(vendor);
                        dbContext.SaveChanges();
                        Console.Write("Vendor Deleted.");                        
                        Console.ReadLine();
                        GetVendors();                        
                    }    
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.Write("Cancelled.");                        
                        Console.ReadLine();
                        GetVendors();     
                    }
                    else
                    {
                        Console.Write("Something went wrong.");
                        Console.ReadLine();
                        GetVendors();
                    }                       
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetVendors();
                }
            } 
        }

        /*end of vendors*/
        /*start of customers*/
        static void GetCustomers()
        {
            Console.Clear();
            Console.WriteLine("CUSTOMER MANAGEMENT");
            Console.WriteLine("[0] ADD CUSTOMER for APPROVAL");
            Console.WriteLine("[1] APPROVE A CUSTOMER");              
            Console.WriteLine("[2] VIEW *CUSTOMERS");
            Console.WriteLine("[3] VIEW *CUSTOMER PER ID");            
            //Console.WriteLine("[4] VIEW *CUSTOMERS PER VENDOR CODE");                      
            Console.WriteLine("[5] UPDATE *CUSTOMERS");          
            Console.WriteLine("[6] DELETE *CUSTOMERS");
            Console.WriteLine("[7] back");
            Console.Write("Enter a number:");
            int num = Convert.ToInt16(Console.ReadLine());
            switch (num)
            {
                case 0:
                AddCustomer();
                break;

                case 1:
                ApproveCustomer();
                break;

                case 2:
                ViewCustomers();
                break;

                case 3:
                ViewCustomerPerId();
                break;   

                case 5:
                UpdateCustomer();
                break;      

                case 6:
                DeleteCustomer();
                break;                                           

                default:
                GetMenu();
                break;
            }
        }

        static void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("APPLICATION OF CUSTOMER FOR APPROVAL");
            Console.Write("Name:");
            string name = Console.ReadLine();
            Console.Write("Address:");
            string address = Console.ReadLine();
            Console.Write("Assign Vendor ID:");
            int vendorID = Convert.ToInt16(Console.ReadLine());            
            Console.WriteLine("Do you wish to save? Y to save N to cancel.");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                using (var dbContext = new CRMSDbContext())
                {
                    var cust = new Customer
                    {
                        Name = name,
                        Address = address,
                        ImageUrl = "nophoto.png",
                        DateCreated = DateTime.Now,
                        VendorId = vendorID,
                        IsApproved = false
                    };
                    dbContext.Add(cust);
                    dbContext.SaveChanges();
                    Console.WriteLine("New Customer Submitted for Approval.");
                    Console.ReadLine();                    
                    GetCustomers();
                }
            }
            else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Not Added");
                Console.ReadLine();
                GetCustomers();
            }
            else
            {
                Console.WriteLine("Something went wrong");
                Console.ReadLine();
                GetCustomers();
            }
        }
        static void ApproveCustomer()
        {
            Console.Clear();
            Console.WriteLine("APPROVAL OF CUSTOMER");
            Console.Write("Customer Id: ");
            int id = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Approve? Y to save N to cancel.");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                using (var dbContext = new CRMSDbContext())
                {
                    var cust = dbContext.Customers.SingleOrDefault(c => c.Id == id);
                    if (cust != null)
                    {
                        cust.IsApproved = true;
                        cust.DateApproved = DateTime.Now;

                        dbContext.SaveChanges();
                        Console.WriteLine(cust.Id+" Approved.");
                        Console.ReadLine();
                        GetCustomers();
                    }
                    else
                    {
                        Console.WriteLine("Not Found.");
                        Console.ReadLine();
                        GetCustomers();
                    }
                }
            }
            else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Not Approved");
                Console.ReadLine();
                GetCustomers();
            }
            else
            {
                Console.WriteLine("Something went wrong");
                Console.ReadLine();
                GetCustomers();
            }
        }        

        static void ViewCustomers()
        {
            Console.Clear();
            Console.WriteLine("APPROVED CUSTOMERS");
            using (var dbContext = new CRMSDbContext())
            {
                var customers = dbContext.Customers.Where(c => c.IsApproved == true);
                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.Id+"\t"+customer.Name+"\t"+customer.Address);
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                GetCustomers();
            }
        }

        static void ViewCustomerPerId()
        {
            Console.Clear();
            Console.WriteLine("VIEW CUSTOMER PER ID");
            Console.Write("Customer Id: ");
            int id = Convert.ToInt16(Console.ReadLine());
            using (var dbContext = new CRMSDbContext())
            {
                var customer = dbContext.Customers.Find(id);
                if (customer != null)
                {
                    Console.WriteLine("Name: "+customer.Name+"\nAddress: "+customer.Address);
                    Console.ReadLine();
                    GetCustomers();
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetCustomers();
                }
            }
        }
        static void UpdateCustomer()
        {
            Console.Clear();
            Console.WriteLine("UPDATE A CUSTOMER");
            Console.Write("Customer Id: ");
            int id = Convert.ToInt16(Console.ReadLine());
            using (var dbContext = new CRMSDbContext())
            {
                var customer = dbContext.Customers.Find(id);
                if (customer != null)
                {
                    Console.WriteLine("Name: "+customer.Name+"\nAddress: "+customer.Address);
                    Console.WriteLine("Update:");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Address: ");
                    string address = Console.ReadLine();      
                    Console.Write("Do you wish to save? Y to Save N to Cancel");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        customer.Name = name;
                        customer.Address = address;

                        dbContext.SaveChanges();
                        Console.Write("Customer Updated.");                        
                        Console.ReadLine();
                        GetCustomers();                        
                    }    
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.Write("No changes made.");                        
                        Console.ReadLine();
                        GetCustomers();     
                    }
                    else
                    {
                        Console.Write("Something went wrong.");
                        Console.ReadLine();
                        GetCustomers();
                    }                       
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetCustomers();
                }
            }            
        }
        static void DeleteCustomer()
        {
            Console.Clear();
            Console.WriteLine("DELETE A CUSTOMER");
            Console.Write("Customer Id: ");
            int id = Convert.ToInt16(Console.ReadLine());
            using (var dbContext = new CRMSDbContext())
            {
                var customer = dbContext.Customers.Find(id);
                if (customer != null)
                {
                    Console.Write("Delete? Y to Save N to Cancel");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        dbContext.Remove(customer);
                        dbContext.SaveChanges();
                        Console.Write("Customer Deleted.");                        
                        Console.ReadLine();
                        GetCustomers();                        
                    }    
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.Write("Cancelled.");                        
                        Console.ReadLine();
                        GetCustomers();     
                    }
                    else
                    {
                        Console.Write("Something went wrong.");
                        Console.ReadLine();
                        GetCustomers();
                    }                       
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetCustomers();
                }
            }            
        }        
        /*end of customers*/

        /*start of categories*/
        static void GetCategories()
        {
            Console.Clear();
            Console.WriteLine("CATEGORIES MANAGEMENT");
            Console.WriteLine("[0] ADD CATEGORY");
            Console.WriteLine("[1] VIEW CATEGORIES");
            Console.WriteLine("[2] UPDATE CATEGORY");
            Console.WriteLine("[3] DELETE CATEGORY");
            Console.WriteLine("[4] back");
            Console.Write("Enter a number:");
            int num = Convert.ToInt16(Console.ReadLine());
            if (num == 0)
            {
                AddCategory();
            }
            else if (num == 1)
            {
                ViewCategories();
            }       
            else if (num == 2)
            {
                UpdateCategory();
            }     
            else if (num == 3)
            {
                DeleteCategory();
            }                            
            else if (num == 4)
            {
                GetMenu();
            }
            else
            {
                Console.WriteLine("Fuck, wrong");
                Console.ReadLine();
                GetMenu();
            }
        }

        static void AddCategory()
        {
            Console.Clear();
            Console.WriteLine("ADD A CATEGORY");
            Console.Write("Category Name:");
            string name = Console.ReadLine();
            Console.Write("Description:");
            string desc = Console.ReadLine();
            Console.WriteLine("Do you wish to save? Y to proceed, N to Cancel.");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                using (var dbContext = new CRMSDbContext())
                {
                    dbContext.Add( new Category
                    {
                        CategoryName = name,
                        CategoryDescription = desc,
                        ImageUrl = "nophoto.png"
                    });
                    dbContext.SaveChanges();
                    GetCategories();
                }
            }
            else if (choice == 'N' || choice == 'n')
            {
                Console.WriteLine("Cancelled");
                Console.ReadLine();
                GetCategories();
            }
            else
            {
                Console.WriteLine("Fuck, wrong");
                Console.ReadLine();
                GetCategories();
            }
        }

        static void ViewCategories()
        {
            Console.Clear();
            Console.WriteLine("CATEGORIES");
            Console.WriteLine("No.\t"+"Category Name\t"+"Description");
            using (var dbContext = new CRMSDbContext())
            {
                var categories = dbContext.Categories;
                foreach (var category in categories)
                {
                    Console.WriteLine(category.Id+"\t"+category.CategoryName+"\t"+category.CategoryDescription);
                }
            }
            Console.WriteLine("Press any key to back");
            Console.ReadLine();
            GetCategories();
        }

        static void UpdateCategory()
        {
            Console.Clear();
            Console.WriteLine("UPDATE A CATEGORY");
            Console.Write("Enter Id:");
            int id = Convert.ToInt16(Console.ReadLine());

            using (var dbContext = new CRMSDbContext())
            {
                var category = dbContext.Categories.Find(id);
                if (category != null)
                {
                    Console.WriteLine(category.CategoryName+"\t"+category.CategoryDescription);
                    Console.WriteLine("Update:");
                    Console.Write("Name:");
                    string name = Console.ReadLine();
                    Console.Write("Description:");
                    string desc = Console.ReadLine();

                    Console.Write("Do you wish to proceed? Y to continue N to Cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());

                    if (choice == 'Y' || choice == 'y')
                    {
                        category.CategoryName = name;
                        category.CategoryDescription = desc;

                        dbContext.SaveChanges();
                        Console.WriteLine("Categories Updated.");
                        Console.ReadLine();
                        GetCategories();
                    }
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.WriteLine("No changes.");
                        Console.ReadLine();
                        GetCategories();
                    }
                    else
                    {
                        Console.ReadLine();
                        GetCategories();
                    }
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetCategories();  
                }
            }
        }
        static void DeleteCategory()
        {
            Console.Clear();
            Console.WriteLine("DELETE A CATEGORY");
            Console.Write("Enter Id:");
            int id = Convert.ToInt16(Console.ReadLine());

            using (var dbContext = new CRMSDbContext())
            {
                var category = dbContext.Categories.Find(id);
                if (category != null)
                {
                    Console.Write("Do you wish to proceed? Y to continue N to Cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());

                    if (choice == 'Y' || choice == 'y')
                    {
                        dbContext.Remove(category);
                        dbContext.SaveChanges();
                        Console.WriteLine("Categories Deleted.");
                        Console.ReadLine();
                        GetCategories();
                    }
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.WriteLine("Cancelled.");
                        Console.ReadLine();
                        GetCategories();
                    }
                    else
                    {
                        Console.ReadLine();
                        GetCategories();
                    }
                }
                else
                {
                    Console.WriteLine("Not Found.");
                    Console.ReadLine();
                    GetCategories();
                }
            }
        }
        /*end of categories*/

/*start of product*/
        static void GetProducts()
        {
            Console.Clear();
            Console.WriteLine("PRODUCTS MANAGEMENT");
            Console.WriteLine("[0] ADD PRODUCTS");
            Console.WriteLine("[1] VIEW ALL PRODUCT");
            Console.WriteLine("[2] VIEW PRODUCT PER PRODUCTCODE");
            Console.WriteLine("[3] VIEW PRODUCT PER CATEGORY");
            Console.WriteLine("[4] UPDATE PRODUCTS");
            Console.WriteLine("[5] UPDATE PRODUCT TO POPULAR");
            Console.WriteLine("[6] DELETE PRODUCTS");
            //Console.WriteLine("[2] VIEW OVERALL SALES PER PRODUCT");            
            Console.WriteLine("[7] back");
            Console.Write("Enter a number:");
            int num = Convert.ToInt16(Console.ReadLine());

            if (num==0)
            {
                AddProduct();
            }
            else if(num==1)
            {
                ViewAllProducts();
            }
            else if(num==2)
            {
                ViewProductPerProductCode();
            }
            else if(num==3)
            {
                ViewProductPerCategory();
            }            
            else if(num==4)
            {
                UpdateProduct();
            }          
            else if(num==5)
            {
                UpdateProductToPopular();
            }    
            else if(num==6)
            {
                DeletePopular();
            }                         
            else if(num==7)
            {
                GetMenu();
            }
            else
            {
                Console.WriteLine("Fuck, wrong");
                Console.ReadLine();
                GetMenu();
            }
        }
        static void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("ADD A PRODUCT");                  
            using (var dbContext = new CRMSDbContext())
            {
                Console.Write("Product Code:");
                string productCode = Console.ReadLine();
                Console.Write("Product Description:");
                string productDesc = Console.ReadLine();
                Console.WriteLine("Categories: ");
                var categories = dbContext.Categories;
                foreach (var category in categories)
                {
                    Console.WriteLine(category.Id+"-"+category.CategoryName+":");
                }
                Console.Write("Category:");                
                int cat = Convert.ToInt16(Console.ReadLine());
                Console.Write("Price:");
                double price = Convert.ToDouble(Console.ReadLine());   
                Console.Write("Do you wish to save? Press Y to Save, N to Cancel");
                char choice = Convert.ToChar(Console.ReadLine());

                if (choice == 'Y' || choice == 'y')
                {
                    dbContext.Add(new Product
                    {
                        ProductCode = productCode,
                        ProductDescription = productDesc,
                        CategoryId = cat,
                        Price = price,
                        DateCreated = DateTime.Now,
                        ImageUrl = "nophoto.png"
                    });
                    dbContext.SaveChanges();
                    GetProducts();
                }
                else if (choice == 'N' || choice == 'n')
                {
                    Console.WriteLine("Cancelled");
                    Console.ReadLine();
                    GetProducts();
                }
                else
                {
                    Console.WriteLine("Fucked, wrong");
                    Console.ReadLine();
                    GetProducts();
                }
            }
        }
        static void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("VIEW ALL PRODUCTS");
            using (var dbContext = new CRMSDbContext())
            {
                var products = dbContext.Products;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductCode+"\t"+product.ProductDescription+"\t"+product.Price);
                }
            }
            Console.WriteLine("Press any key to back");
            Console.ReadLine();
            GetProducts();
        }
        static void ViewProductPerProductCode()
        {
            Console.Clear();
            Console.WriteLine("VIEW PRODUCT PER PRODUCT CODE");
            Console.Write("Product Code: ");
            string productCode = Console.ReadLine();
            using (var dbContext = new CRMSDbContext())
            {
                var product = dbContext.Products.SingleOrDefault(p => p.ProductCode == productCode);
                if (product != null)
                {
                    Console.Write(product.ProductCode+"\n"+product.ProductDescription+"\n"+product.IsPopular);
                    Console.ReadLine();
                    GetProducts();
                }
                else
                {
                    Console.WriteLine("Not Found");
                    Console.ReadLine();
                    GetProducts();
                }
            }
        }
        static void ViewProductPerCategory()
        {
            Console.Clear();
            Console.WriteLine("VIEW PRODUCT PER CATEGORY");
            using (var dbContext = new CRMSDbContext())
            {
                Console.WriteLine("Categories:");
                var categories = dbContext.Categories;
                foreach (var category in categories)
                {
                    Console.WriteLine(category.Id+"-"+category.CategoryName+"\t");
                }
                Console.Write("Category: ");
                int catId = Convert.ToInt16(Console.ReadLine());

                var products = from p in dbContext.Products.Where(P => P.CategoryId == catId)
                                join c in dbContext.Categories on p.CategoryId equals c.Id
                                select new
                                {
                                    Id = p.Id,
                                    ProductCode = p.ProductCode,
                                    ProductName = p.ProductDescription,
                                    CategoryName = c.CategoryName
                                };
                                foreach (var product in products)
                                {
                                    Console.WriteLine(product.Id+"\t"+product.ProductCode+"\t"+product.ProductName+"\t"+product.CategoryName);
                                }
            }
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
                GetProducts();                    
        }
        static void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("UPDATE A PRODUCT");
            Console.Write("Product Code: ");
            string productCode = Console.ReadLine();
            using (var dbContext = new CRMSDbContext())
            {
                var product = dbContext.Products.SingleOrDefault(p => p.ProductCode == productCode);
                if (product != null)
                {
                    Console.Write("Product Description:");
                    string productDesc = Console.ReadLine();
                    var categories = dbContext.Categories;
                    foreach (var category in categories)
                    {
                        Console.WriteLine("Categories "+category.Id+"-"+category.CategoryName+":");
                    }
                    Console.Write("Category:");                
                    int cat = Convert.ToInt16(Console.ReadLine());
                    Console.Write("Price:");
                    double price = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Do you wish to update? Y to update, N to cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        product.ProductDescription = productDesc;
                        product.CategoryId = cat;
                        product.Price = price;

                        dbContext.SaveChangesAsync();
                        Console.WriteLine("Product Updated.");
                        Console.ReadLine();
                        GetProducts();
                    }
                }
                else
                {
                    Console.WriteLine("Not Found");
                    Console.ReadLine();
                    GetProducts();
                }
            }
        }
        static void UpdateProductToPopular()
        {
            Console.Clear();
            Console.WriteLine("UPDATE A PRODUCT");
            Console.Write("Product Code: ");
            string productCode = Console.ReadLine();
            using (var dbContext = new CRMSDbContext())
            {
                var product = dbContext.Products.SingleOrDefault(p => p.ProductCode == productCode);
                if (product != null)
                {             
                    Console.WriteLine("Is Popular? Y to update, N to cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        product.IsPopular = true;

                        dbContext.SaveChangesAsync();
                        Console.WriteLine("Product Updated.");
                        Console.ReadLine();
                        GetProducts();
                    }
                    else if (choice == 'N' || choice == 'n')
                    {
                        product.IsPopular = false;

                        dbContext.SaveChangesAsync();
                        Console.WriteLine("Product Updated.");
                        Console.ReadLine();
                        GetProducts();
                    }
                    else
                    {
                        Console.WriteLine("Wrong, press any key to continue.");
                        Console.ReadLine();
                        GetProducts();
                    }
                }
                else
                {
                    Console.WriteLine("Not Found");
                    Console.ReadLine();
                    GetProducts();
                }
            }
        }
        static void DeletePopular()
        {
            Console.Clear();
            Console.WriteLine("DELETE A PRODUCT");
            Console.Write("Product Code: ");
            string productCode = Console.ReadLine();
            using (var dbContext = new CRMSDbContext())
            {
                var product = dbContext.Products.SingleOrDefault(p => p.ProductCode == productCode);
                if (product != null)
                {
                    Console.WriteLine("Do you wish to Delete? Y to update, N to cancel.");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        dbContext.Remove(product);
                        dbContext.SaveChanges();
                        Console.WriteLine("Product Deleted.");
                        Console.ReadLine();
                        GetProducts();
                    }
                    else if (choice == 'N' || choice == 'n')
                    {
                        Console.WriteLine("Cancelled. No changes made.");
                        Console.ReadLine();
                        GetProducts();
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong. Try Again");
                        Console.ReadLine();
                        GetProducts();
                    }
                }
                else
                {
                    Console.WriteLine("Not Found");
                    Console.ReadLine();
                    GetProducts();
                }
            }
        }
        /*end of product*/
    }
}

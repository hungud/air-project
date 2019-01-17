namespace App.Base
{
    namespace GOFDPArchitectureStructure
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
        namespace GOFDPArchitectureStructure
        {
            /// <summary>
            /// 
            /// </summary>
            public class GOFDPArchitectureStructureSample
            {
                public GOFDPArchitectureStructureSample()
                {

                    //string strToken = App.Base.TokenManager.BaseTokenSecurityManager.GenerateToken("RakeshPal", "RMSIGIS", "117.0.0.125", "RMSI", 500);
                    //bool ValidToken = App.Base.TokenManager.BaseTokenSecurityManager.IsTokenValid(strToken, "117.0.0.125", "RMSI");

                    ///
                    ///CreationalDesign
                    ///
                    new CreationalDesign.SingletonSample.SingletonSample().GetSingletonSample();
                    new CreationalDesign.FactorySample.FactorySample().GetFactoryData();
                    new CreationalDesign.AbstractFactorySample.AbstractFactorySample().GetAbstractFactoryData();
                    new CreationalDesign.BuilderDesignSample.BuilderDesignSample().GetBuilderDesignData();
                    new CreationalDesign.PrototypeDesignSample.PrototypeDesignSample().GetPrototypeDesignSampleData();
                    ///
                    ///StructuralDesign
                    ///
                    new StructuralDesign.AdapterDesignSample.AdapterDesignSample().GetAdapterDesignData();
                    new StructuralDesign.BridgeDesignSample.BridgeDesignSample().GetBridgeDesignData();
                    new StructuralDesign.CompositeDesignSample.CompositeDesignSample().GetCompositeDesignData();
                    new StructuralDesign.DecoratorDesignSample.DecoratorDesignSample().GetDecoratorDesignData();
                    new StructuralDesign.FacadeDesignSample.FacadeDesignSample().GetFacadeDesignData();
                    new StructuralDesign.FlyweightDesignSample.FlyweightDesignSample().GetFlyweightDesignData();
                    new StructuralDesign.ProxySample.ProxySample().GetProxyData();
                    ///
                    ///BehavioralDesign
                    ///
                    new BehavioralDesign.ChainofResponsibilityDesignSample.ChainofResponsibilityDesignSample().GetChainofResponsibilityDesignData();
                    new BehavioralDesign.ChainofResponsibilityDesignSample1.ChainofResponsibilityDesignSample().GetChainofResponsibilityDesignData();
                    new BehavioralDesign.ChainofResponsibilityDesignSample2.ChainofResponsibilityDesignSample().GetChainofResponsibilityDesignData();
                    new BehavioralDesign.CommandDesignSample.CommandDesignSample().GetCommandDesignData();
                    new BehavioralDesign.CommandDesignSample1.CommandDesignSample().GetCommandDesignData();
                    new BehavioralDesign.CommandDesignSample2.CommandDesignSample().GetCommandDesignData();
                    new BehavioralDesign.InterpreterSamle.InterpreterSamle().GetInterpreterData();
                    new BehavioralDesign.IteratorSample.IteratorSample().GetIteratorData();
                    new BehavioralDesign.IteratorSample1.IteratorSample().GetIteratorData();
                    new BehavioralDesign.MediatorSample.MediatorSample().GetMediatorData();
                    new BehavioralDesign.MediatorSample1.MediatorSample().GetMediatorData();
                    new BehavioralDesign.MementoSample.MementoSample().GetMementoData();
                    new BehavioralDesign.ObserverSample.ObserverSample().GetObserverData();
                    new BehavioralDesign.ObserverSample1.ObserverSample().GetObserverData();
                    new BehavioralDesign.StateSample.StateSample().GetStateData();
                    new BehavioralDesign.StateSample1.StateSample().GetStateData();
                    new BehavioralDesign.StrategySample.StrategySample().GetStrategyData();
                    new BehavioralDesign.StrategySample1.StrategySample().GetStrategyData();
                    new BehavioralDesign.VisitorSample.VisitorSample().GetVisitorData();
                    new BehavioralDesign.VisitorSample1.VisitorSample().GetVisitorData();
                    new BehavioralDesign.TemplateMethodSample.TemplateMethodSample().GetTemplateMethodData();

                    ///
                    ///OOPS Samples
                    ///
                    new OOPS.OOPSHelp();

                }
            }

            ///CreationalDesign
            namespace CreationalDesign
            {
                namespace SingletonSample
                {
                    /// <summary>
                    /// Singleton pattern falls under Creational Pattern of Gang of Four (GOF) Design Patterns in .Net. It is pattern is one of the simplest design patterns. This pattern ensures that a class has only one instance. In this article, I would like share what is Singleton pattern and how is it work?
                    /// What is Singleton Pattern?
                    /// Singleton pattern is one of the simplest design patterns. This pattern ensures that a class has only one instance and provides a global point of access to it.
                    /// </summary>
                    public sealed class Singleton
                    {

                        /// <summary>
                        ///*************************************************
                        /// Developed By:   RAKESH PAL                
                        /// Company Name:             
                        /// Created Date:   Developed on:            
                        /// Summary :ErrorsLog
                        ///*************************************************
                        /// </summary>

                        private static volatile Singleton SingletonInstance;
                        private static object syncRoot = new Object();
                        private Singleton() { }
                        public static Singleton ErrorsLogInstance
                        {
                            get
                            {
                                if (SingletonInstance == null)
                                {
                                    lock (syncRoot)
                                    {
                                        if (SingletonInstance == null)
                                            SingletonInstance = new Singleton();
                                    }
                                }

                                return SingletonInstance;
                            }
                        }

                        public static Singleton Create()
                        {
                            if (SingletonInstance == null)
                            {
                                SingletonInstance = new Singleton();
                                return SingletonInstance;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        public double ValueOne { get; set; }
                        public double ValueTwo { get; set; }

                        public double Addition()
                        {
                            return ValueOne + ValueTwo;
                        }

                        public double Subtraction()
                        {
                            return ValueOne - ValueTwo;
                        }

                        public double Multiplication()
                        {
                            return ValueOne * ValueTwo;
                        }

                        public double Division()
                        {
                            return ValueOne / ValueTwo;
                        }


                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="Exce"></param>
                        public void ManageException(Exception Exce)
                        {
                            string ErrorMessage = "|| EXMESSAGE ||:- " + Exce.Message + "  || EXSOURCE ||:- " + Exce.Source + " || EXTARGETSITE ||:- " + Exce.TargetSite + "  ||  EXData ||:- " + Exce.Data + Environment.NewLine + Environment.NewLine + Environment.NewLine + "||EXInnerException||:-  " + Environment.NewLine + Exce.InnerException;
                            LogMessage(ErrorMessage.ToString());
                        }
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="message"></param>
                        public void LogMessage(string message)
                        {

                        }
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="message"></param>
                        public void LogEventMessage(string message)
                        {

                        }
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="StrUserDetails"></param>
                        /// <param name="StrUserMessageDetails"></param>
                        public void LogUserEventLog(string StrUserDetails, string StrUserMessageDetails)
                        {

                        }
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="message"></param>
                        public void LogEventTrape(string message)
                        {

                        }
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="DirectoryPath"></param>
                        private void CreateValicdateDirectory(string DirectoryPath)
                        {
                            string[] pathParts = DirectoryPath.Split('\\');
                            IList<string> PathList = pathParts;
                            List<String> list = new List<string>(pathParts);

                            for (int i = 0; i < pathParts.Length; i++)
                            {
                                if (i > 0)
                                    pathParts[i] = Path.Combine(pathParts[i - 1], pathParts[i]);

                                if (!Directory.Exists(pathParts[i]))
                                    Directory.CreateDirectory(pathParts[i]);
                            }
                        }
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="path"></param>
                        /// <param name="indent"></param>
                        private void ShowAllFoldersUnder(string path, int indent)
                        {
                            foreach (string folder in Directory.GetDirectories(path))
                            {
                                Console.WriteLine("{0}{1}", new string(' ', indent), Path.GetFileName(folder));
                                ShowAllFoldersUnder(folder, indent + 2);
                            }
                        }
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="sDir"></param>
                        /// <param name="path"></param>
                        private void DirSearch(string sDir, string path)
                        {
                            try
                            {
                                System.Collections.Generic.List<string> MyData = new System.Collections.Generic.List<string>();
                                foreach (string d in Directory.GetDirectories(sDir))
                                {
                                    foreach (string f in Directory.GetFiles(d, path))
                                    {
                                        MyData.Add(f);
                                    }
                                    DirSearch(d, path);
                                }
                            }
                            catch (System.Exception excpt)
                            {
                                Console.WriteLine(excpt.Message);
                            }
                        }
                    }
                    /// <summary>
                    /// SingletonSample
                    /// </summary>
                    public class SingletonSample
                    {
                        public void GetSingletonSample()
                        {
                            Singleton.ErrorsLogInstance.ValueOne = 10.5;
                            Singleton.ErrorsLogInstance.ValueTwo = 5.5;
                            Console.WriteLine("Addition : " + Singleton.ErrorsLogInstance.Addition());
                            Console.WriteLine("Subtraction : " + Singleton.ErrorsLogInstance.Subtraction());
                            Console.WriteLine("Multiplication : " + Singleton.ErrorsLogInstance.Multiplication());
                            Console.WriteLine("Division : " + Singleton.ErrorsLogInstance.Division());


                            Singleton.ErrorsLogInstance.ValueTwo = 10.5;
                            Console.WriteLine("Addition : " + Singleton.ErrorsLogInstance.Addition());
                            Console.WriteLine("Subtraction : " + Singleton.ErrorsLogInstance.Subtraction());
                            Console.WriteLine("Multiplication : " + Singleton.ErrorsLogInstance.Multiplication());
                            Console.WriteLine("Division : " + Singleton.ErrorsLogInstance.Division());

                            Singleton sic1, sic2;
                            sic1 = Singleton.Create();
                            if (sic1 != null)
                                Console.WriteLine("OK");
                            sic2 = Singleton.Create();
                            if (sic2 == null)
                                Console.WriteLine("NO MORE OBJECTS");
                        }
                    }
                }
                namespace FactorySample
                {
                    /// <summary>
                    /// Factory  method pattern falls under Creational Pattern of Gang of Four (GOF) Design Patterns in .Net. It is used to create objects. People usually use this pattern as the standard way to create objects. In this article, I would like share what is factory pattern and how is it work?
                    /// What is Factory Method Pattern?
                    /// In Factory pattern, we create object without exposing the creation logic. In this pattern, an interface is used for creating an object, but let subclass decide which class to instantiate. The creation of object is done when it is required. The Factory method allows a class later instantiation to subclasses.
                    /// </summary>
                    public class FactorySample
                    {
                        public void GetFactoryData()
                        {
                            VehicleFactory factory = new ConcreteVehicleFactory();
                            IFactory scooter = factory.GetVehicle("Scooter");
                            scooter.Drive(10);
                            IFactory bike = factory.GetVehicle("Bike");
                            bike.Drive(20);
                            IFactory car = factory.GetVehicle("Car");
                            bike.Drive(120);
                        }
                    }
                    /// <summary>
                    /// The 'Product' interface
                    /// </summary>
                    public interface IFactory
                    {
                        void Drive(int miles);
                    }
                    /// <summary>
                    /// A 'ConcreteProduct' class
                    /// </summary>
                    public class Scooter : IFactory
                    {
                        public void Drive(int miles)
                        {
                            Console.WriteLine("Drive the Scooter : " + miles.ToString() + "km");
                        }
                    }
                    /// <summary>
                    /// A 'ConcreteProduct' class
                    /// </summary>
                    public class Bike : IFactory
                    {
                        public void Drive(int miles)
                        {
                            Console.WriteLine("Drive the Bike : " + miles.ToString() + "km");
                        }
                    }
                    /// <summary>
                    /// A 'ConcreteProduct' class
                    /// </summary>
                    public class Car : IFactory
                    {
                        public void Drive(int miles)
                        {
                            Console.WriteLine("Drive the Car : " + miles.ToString() + "km");
                        }
                    }
                    /// <summary>
                    /// The Creator Abstract Class
                    /// </summary>
                    public abstract class VehicleFactory
                    {
                        public abstract IFactory GetVehicle(string Vehicle);

                    }
                    /// <summary>
                    /// A 'ConcreteCreator' class
                    /// </summary>
                    public class ConcreteVehicleFactory : VehicleFactory
                    {
                        public override IFactory GetVehicle(string Vehicle)
                        {
                            switch (Vehicle)
                            {
                                case "Scooter":
                                    return new Scooter();
                                case "Bike":
                                    return new Bike();
                                case "Car":
                                    return new Car();
                                default:
                                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", Vehicle));
                            }
                        }
                    }
                }
                namespace AbstractFactorySample
                {
                    /// <summary>
                    /// The 'AbstractFactory' Factory method pattern falls under Creational Pattern of Gang of Four (GOF) Design Patterns in .Net. It is used to create a set of related objects, or dependent objects. Internally, Abstract Factory use Factory design pattern for creating objects. It may also use Builder design pattern and prototype design pattern for creating objects. It completely depends upon your implementation for creating objects. In this article, I would like share what is abstract factory pattern and how is it work?
                    /// What is Abstract Factory Pattern?
                    /// Abstract Factory patterns acts a super-factory which creates other factories. This pattern is also called as Factory of factories. In Abstract Factory pattern an interface is responsible for creating a set of related objects, or dependent objects without specifying their concrete classes.
                    /// </summary>
                    public interface VehicleFactory
                    {
                        Bike GetBike(string Bike);
                        Scooter GetScooter(string Scooter);
                    }
                    /// <summary>
                    /// The 'ConcreteFactory1' class.
                    /// </summary>
                    public class HondaFactory : VehicleFactory
                    {
                        public Bike GetBike(string Bike)
                        {
                            switch (Bike)
                            {
                                case "Sports":
                                    return new SportsBike();
                                case "Regular":
                                    return new RegularBike();
                                default:
                                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", Bike));
                            }
                        }
                        public Scooter GetScooter(string Scooter)
                        {
                            switch (Scooter)
                            {
                                case "Sports":
                                    return new Scooty();
                                case "Regular":
                                    return new RegularScooter();
                                default:
                                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", Scooter));
                            }
                        }
                    }
                    /// <summary>
                    /// The 'ConcreteFactory2' class.
                    /// </summary>
                    public class HeroFactory : VehicleFactory
                    {
                        public Bike GetBike(string Bike)
                        {
                            switch (Bike)
                            {
                                case "Sports":
                                    return new SportsBike();
                                case "Regular":
                                    return new RegularBike();
                                default:
                                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", Bike));
                            }
                        }
                        public Scooter GetScooter(string Scooter)
                        {
                            switch (Scooter)
                            {
                                case "Sports":
                                    return new Scooty();
                                case "Regular":
                                    return new RegularScooter();
                                default:
                                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", Scooter));
                            }
                        }
                    }
                    /// <summary>
                    /// The 'AbstractProductA' interface
                    /// </summary>
                    public interface Bike
                    {
                        string Name();
                    }
                    /// <summary>
                    /// The 'AbstractProductB' interface
                    /// </summary>
                    public interface Scooter
                    {
                        string Name();
                    }
                    /// <summary>
                    /// The 'ProductA1' class
                    /// </summary>
                    public class RegularBike : Bike
                    {
                        public string Name()
                        {
                            return "Regular Bike- Name";
                        }
                    }
                    /// <summary>
                    /// The 'ProductA2' class
                    /// </summary>
                    public class SportsBike : Bike
                    {
                        public string Name()
                        {
                            return "Sports Bike- Name";
                        }
                    }
                    /// <summary>
                    /// The 'ProductB1' class
                    /// </summary>
                    public class RegularScooter : Scooter
                    {
                        public string Name()
                        {
                            return "Regular Scooter- Name";
                        }
                    }
                    /// <summary>
                    /// The 'ProductB2' class
                    /// </summary>
                    public class Scooty : Scooter
                    {
                        public string Name()
                        {
                            return "Scooty- Name";
                        }
                    }
                    /// <summary>
                    /// The 'Client' class 
                    /// </summary>
                    public class VehicleClient
                    {
                        Bike bike;
                        Scooter scooter;

                        public VehicleClient(VehicleFactory factory, string type)
                        {
                            bike = factory.GetBike(type);
                            scooter = factory.GetScooter(type);
                        }

                        public string GetBikeName()
                        {
                            return bike.Name();
                        }

                        public string GetScooterName()
                        {
                            return scooter.Name();
                        }

                    }
                    /// <summary>
                    /// Abstract Factory Pattern Demo
                    /// </summary>
                    public class AbstractFactorySample
                    {
                        public void GetAbstractFactoryData()
                        {
                            VehicleFactory honda = new HondaFactory();
                            VehicleClient hondaclient = new VehicleClient(honda, "Regular");

                            Console.WriteLine("******* Honda **********");
                            Console.WriteLine(hondaclient.GetBikeName());
                            Console.WriteLine(hondaclient.GetScooterName());

                            hondaclient = new VehicleClient(honda, "Sports");
                            Console.WriteLine(hondaclient.GetBikeName());
                            Console.WriteLine(hondaclient.GetScooterName());

                            VehicleFactory hero = new HeroFactory();
                            VehicleClient heroclient = new VehicleClient(hero, "Regular");

                            Console.WriteLine("******* Hero **********");
                            Console.WriteLine(heroclient.GetBikeName());
                            Console.WriteLine(heroclient.GetScooterName());

                            heroclient = new VehicleClient(hero, "Sports");
                            Console.WriteLine(heroclient.GetBikeName());
                            Console.WriteLine(heroclient.GetScooterName());

                        }
                    }
                }
                namespace BuilderDesignSample
                {
                    /// <summary>
                    /// What is Builder Pattern?
                    /// Builder pattern builds a complex object by using a step by step approach. Builder interface defines the steps to build the final object. This builder is independent from the objects creation process. A class that is known as Director, controls the object creation process.
                    /// Moreover, builder pattern describes a way to separate an object from its construction. The same construction method can create different representation of the object.
                    /// </summary>
                    public interface IVehicleBuilder
                    {
                        void SetModel();
                        void SetEngine();
                        void SetTransmission();
                        void SetBody();
                        void SetAccessories();

                        Vehicle GetVehicle();
                    }
                    /// <summary>
                    /// The 'ConcreteBuilder1' class
                    /// </summary>
                    public class HeroBuilder : IVehicleBuilder
                    {
                        Vehicle objVehicle = new Vehicle();
                        public void SetModel()
                        {
                            objVehicle.Model = "Hero";
                        }

                        public void SetEngine()
                        {
                            objVehicle.Engine = "4 Stroke";
                        }

                        public void SetTransmission()
                        {
                            objVehicle.Transmission = "120 km/hr";
                        }

                        public void SetBody()
                        {
                            objVehicle.Body = "Plastic";
                        }

                        public void SetAccessories()
                        {
                            objVehicle.Accessories.Add("Seat Cover");
                            objVehicle.Accessories.Add("Rear Mirror");
                        }

                        public Vehicle GetVehicle()
                        {
                            return objVehicle;
                        }
                    }
                    /// <summary>
                    /// The 'ConcreteBuilder2' class
                    /// </summary>
                    public class HondaBuilder : IVehicleBuilder
                    {
                        Vehicle objVehicle = new Vehicle();
                        public void SetModel()
                        {
                            objVehicle.Model = "Honda";
                        }

                        public void SetEngine()
                        {
                            objVehicle.Engine = "4 Stroke";
                        }

                        public void SetTransmission()
                        {
                            objVehicle.Transmission = "125 Km/hr";
                        }

                        public void SetBody()
                        {
                            objVehicle.Body = "Plastic";
                        }

                        public void SetAccessories()
                        {
                            objVehicle.Accessories.Add("Seat Cover");
                            objVehicle.Accessories.Add("Rear Mirror");
                            objVehicle.Accessories.Add("Helmet");
                        }

                        public Vehicle GetVehicle()
                        {
                            return objVehicle;
                        }
                    }
                    /// <summary>
                    /// The 'Product' class
                    /// </summary>
                    public class Vehicle
                    {
                        public string Model { get; set; }
                        public string Engine { get; set; }
                        public string Transmission { get; set; }
                        public string Body { get; set; }
                        public List<string> Accessories { get; set; }

                        public Vehicle()
                        {
                            Accessories = new List<string>();
                        }

                        public void ShowInfo()
                        {
                            Console.WriteLine("Model: {0}", Model);
                            Console.WriteLine("Engine: {0}", Engine);
                            Console.WriteLine("Body: {0}", Body);
                            Console.WriteLine("Transmission: {0}", Transmission);
                            Console.WriteLine("Accessories:");
                            foreach (var accessory in Accessories)
                            {
                                Console.WriteLine("\t{0}", accessory);
                            }
                        }
                    }
                    /// <summary>
                    /// The 'Director' class
                    /// </summary>
                    public class VehicleCreator
                    {
                        private readonly IVehicleBuilder objBuilder;

                        public VehicleCreator(IVehicleBuilder builder)
                        {
                            objBuilder = builder;
                        }

                        public void CreateVehicle()
                        {
                            objBuilder.SetModel();
                            objBuilder.SetEngine();
                            objBuilder.SetBody();
                            objBuilder.SetTransmission();
                            objBuilder.SetAccessories();
                        }

                        public Vehicle GetVehicle()
                        {
                            return objBuilder.GetVehicle();
                        }
                    }
                    /// <summary>
                    /// Builder Design Pattern Demo
                    /// </summary>
                    public class BuilderDesignSample
                    {
                        public void GetBuilderDesignData()
                        {
                            var vehicleCreator = new VehicleCreator(new HeroBuilder());
                            vehicleCreator.CreateVehicle();
                            var vehicle = vehicleCreator.GetVehicle();
                            vehicle.ShowInfo();

                            Console.WriteLine("---------------------------------------------");

                            vehicleCreator = new VehicleCreator(new HondaBuilder());
                            vehicleCreator.CreateVehicle();
                            vehicle = vehicleCreator.GetVehicle();
                            vehicle.ShowInfo();

                            Console.ReadKey();
                        }
                    }
                }
                namespace PrototypeDesignSample
                {
                    /// <summary>
                    /// Prototype pattern falls under Creational Pattern of Gang of Four (GOF) Design Patterns in .Net. It is used to used to create a duplicate object or clone of the current object. It provides an interface for creating parts of a product. In this article, I would like share what is Prototype pattern and how is it work?
                    /// What is Prototype Pattern?
                    /// Prototype pattern is used to create a duplicate object or clone of the current object to enhance performance. This pattern is used when creation of object is costly or complex.
                    /// For Example: An object is to be created after a costly database operation. We can cache the object, returns its clone on next request and update the database as and when needed thus reducing database calls.
                    /// </summary>
                    /// <summary>
                    /// The 'Prototype' interface
                    /// </summary>
                    public interface IEmployee
                    {
                        IEmployee Clone();
                        string GetDetails();
                    }
                    /// <summary>
                    /// A 'ConcretePrototype' class
                    /// </summary>
                    public class Developer : IEmployee
                    {
                        public int WordsPerMinute { get; set; }
                        public string Name { get; set; }
                        public string Role { get; set; }
                        public string PreferredLanguage { get; set; }

                        public IEmployee Clone()
                        {
                            // Shallow Copy: only top-level objects are duplicated
                            return (IEmployee)MemberwiseClone();

                            // Deep Copy: all objects are duplicated
                            //return (IEmployee)this.Clone();
                        }

                        public string GetDetails()
                        {
                            return string.Format("{0} - {1} - {2}", Name, Role, PreferredLanguage);
                        }
                    }
                    /// <summary>
                    /// A 'ConcretePrototype' class
                    /// </summary>
                    public class Typist : IEmployee
                    {
                        public int WordsPerMinute { get; set; }
                        public string Name { get; set; }
                        public string Role { get; set; }

                        public IEmployee Clone()
                        {
                            // Shallow Copy: only top-level objects are duplicated
                            return (IEmployee)MemberwiseClone();

                            // Deep Copy: all objects are duplicated
                            //return (IEmployee)this.Clone();
                        }

                        public string GetDetails()
                        {
                            return string.Format("{0} - {1} - {2}wpm", Name, Role, WordsPerMinute);
                        }
                    }
                    /// <summary>
                    /// Prototype Pattern Demo
                    /// </summary>
                    public class PrototypeDesignSample
                    {
                        public void GetPrototypeDesignSampleData()
                        {
                            Developer dev = new Developer();
                            dev.Name = "Rahul";
                            dev.Role = "Team Leader";
                            dev.PreferredLanguage = "C#";

                            Developer devCopy = (Developer)dev.Clone();
                            devCopy.Name = "Arif"; //Not mention Role and PreferredLanguage, it will copy above

                            Console.WriteLine(dev.GetDetails());
                            Console.WriteLine(devCopy.GetDetails());

                            Typist typist = new Typist();
                            typist.Name = "Monu";
                            typist.Role = "Typist";
                            typist.WordsPerMinute = 120;

                            Typist typistCopy = (Typist)typist.Clone();
                            typistCopy.Name = "Sahil";
                            typistCopy.WordsPerMinute = 115;//Not mention Role, it will copy above

                            Console.WriteLine(typist.GetDetails());
                            Console.WriteLine(typistCopy.GetDetails());
                        }
                    }
                }
            }
            ///StructuralDesign
            namespace StructuralDesign
            {
                namespace AdapterDesignSample
                {
                    /// <summary>
                    /// Adapter pattern falls under Structural Pattern of Gang of Four (GOF) Design Patterns in .Net. The Adapter pattern allows a system to use classes of another system that is incompatible with it. It is especially used for toolkits and libraries. In this article, I would like share what is adapter pattern and how is it work?
                    /// What is Adapter Pattern
                    /// Adapter pattern acts as a bridge between two incompatible interfaces. This pattern involves a single class called adapter which is responsible for communication between two independent or incompatible interfaces.
                    /// For Example: A card reader acts as an adapter between memory card and a laptop. You plugins the memory card into card reader and card reader into the laptop so that memory card can be read via laptop.
                    /// </summary>
                    public class ThirdPartyBillingSystem
                    {
                        private ITarget employeeSource;

                        public ThirdPartyBillingSystem(ITarget employeeSource)
                        {
                            this.employeeSource = employeeSource;
                        }

                        public void ShowEmployeeList()
                        {
                            List<string> employee = employeeSource.GetEmployeeList();
                            //To DO: Implement you business logic

                            Console.WriteLine("######### Employee List ##########");
                            foreach (var item in employee)
                            {
                                Console.Write(item);
                            }

                        }
                    }
                    /// <summary>
                    /// The 'ITarget' interface
                    /// </summary>
                    public interface ITarget
                    {
                        List<string> GetEmployeeList();
                    }
                    /// <summary>
                    /// The 'Adaptee' class
                    /// </summary>
                    public class HRSystem
                    {
                        public string[][] GetEmployees()
                        {
                            string[][] employees = new string[4][];

                            employees[0] = new string[] { "100", "Deepak", "Team Leader" };
                            employees[1] = new string[] { "101", "Rohit", "Developer" };
                            employees[2] = new string[] { "102", "Gautam", "Developer" };
                            employees[3] = new string[] { "103", "Dev", "Tester" };

                            return employees;
                        }
                    }
                    /// <summary>
                    /// The 'Adapter' class
                    /// </summary>
                    public class EmployeeAdapter : HRSystem, ITarget
                    {
                        public List<string> GetEmployeeList()
                        {
                            List<string> employeeList = new List<string>();
                            string[][] employees = GetEmployees();
                            foreach (string[] employee in employees)
                            {
                                employeeList.Add(employee[0]);
                                employeeList.Add(",");
                                employeeList.Add(employee[1]);
                                employeeList.Add(",");
                                employeeList.Add(employee[2]);
                                employeeList.Add("\n");
                            }

                            return employeeList;
                        }
                    }
                    /// 
                    /// Adapter Design Pattern Demo
                    /// 
                    public class AdapterDesignSample
                    {
                        public void GetAdapterDesignData()
                        {
                            ITarget Itarget = new EmployeeAdapter();
                            ThirdPartyBillingSystem client = new ThirdPartyBillingSystem(Itarget);
                            client.ShowEmployeeList();
                        }
                    }
                }
                namespace BridgeDesignSample
                {
                    /// <summary>
                    /// The 'Bridge' pattern falls under Structural Pattern of Gang of Four (GOF) Design Patterns in .Net. All we know, Inheritance is a way to specify different implementations of an abstraction. But in this way, implementations are tightly bound to the abstraction and can not be modified independently. The Bridge pattern provides an alternative to inheritance when there are more than one version of an abstraction. In this article, I would like share what is bridge pattern and how is it work?
                    /// What is Bridge Pattern
                    /// Bridge pattern is used to separate an abstraction from its implementation so that both can be modified independently.
                    /// This pattern involves an interface which acts as a bridge between the abstraction class and implementer classes and also makes the functionality of implementer class independent from the abstraction class. Both types of classes can be modified without affecting to each other.
                    /// </summary>
                    public abstract class Message
                    {
                        public IMessageSender MessageSender { get; set; }
                        public string Subject { get; set; }
                        public string Body { get; set; }
                        public abstract void Send();
                    }
                    /// <summary>
                    /// The 'RefinedAbstraction' class
                    /// </summary>
                    public class SystemMessage : Message
                    {
                        public override void Send()
                        {
                            MessageSender.SendMessage(Subject, Body);
                        }
                    }
                    /// <summary>
                    /// The 'RefinedAbstraction' class
                    /// </summary>
                    public class UserMessage : Message
                    {
                        public string UserComments { get; set; }

                        public override void Send()
                        {
                            string fullBody = string.Format("{0}\nUser Comments: {1}", Body, UserComments);
                            MessageSender.SendMessage(Subject, fullBody);
                        }
                    }
                    /// <summary>
                    /// The 'Bridge/Implementor' interface
                    /// </summary>
                    public interface IMessageSender
                    {
                        void SendMessage(string subject, string body);
                    }
                    /// <summary>
                    /// The 'ConcreteImplementor' class
                    /// </summary>
                    public class EmailSender : IMessageSender
                    {
                        public void SendMessage(string subject, string body)
                        {
                            Console.WriteLine("Email\n{0}\n{1}\n", subject, body);
                        }
                    }
                    /// <summary>
                    /// The 'ConcreteImplementor' class
                    /// </summary>
                    public class MSMQSender : IMessageSender
                    {
                        public void SendMessage(string subject, string body)
                        {
                            Console.WriteLine("MSMQ\n{0}\n{1}\n", subject, body);
                        }
                    }
                    /// <summary>
                    /// The 'ConcreteImplementor' class
                    /// </summary>
                    public class WebServiceSender : IMessageSender
                    {
                        public void SendMessage(string subject, string body)
                        {
                            Console.WriteLine("Web Service\n{0}\n{1}\n", subject, body);
                        }
                    }
                    /// <summary>
                    /// Bridge Design Pattern Demo
                    /// </summary>
                    public class BridgeDesignSample
                    {
                        public void GetBridgeDesignData()
                        {
                            IMessageSender email = new EmailSender();
                            IMessageSender queue = new MSMQSender();
                            IMessageSender web = new WebServiceSender();

                            Message message = new SystemMessage();
                            message.Subject = "Test Message";
                            message.Body = "Hi, This is a Test Message";

                            message.MessageSender = email;
                            message.Send();

                            message.MessageSender = queue;
                            message.Send();

                            message.MessageSender = web;
                            message.Send();

                            UserMessage usermsg = new UserMessage();
                            usermsg.Subject = "Test Message";
                            usermsg.Body = "Hi, This is a Test Message";
                            usermsg.UserComments = "I hope you are well";

                            usermsg.MessageSender = email;
                            usermsg.Send();

                            Console.ReadKey();
                        }
                    }
                }
                namespace CompositeDesignSample
                {
                    /// <summary>
                    /// The 'Component' pattern falls under Structural Pattern of Gang of Four (GOF) Design Patterns in .Net. Composite Pattern is used to arrange structured hierarchies. In this article, I would like share what is composite pattern and how is it work?
                    /// What is Composite Pattern
                    /// Composite pattern is used to separate an abstraction from its implementation so that both can be modified independently.
                    /// Composite pattern is used when we need to treat a group of objects and a single object in the same way. Composite pattern composes objects in term of a tree structure to represent part as well as whole hierarchies.
                    /// This pattern creates a class contains group of its own objects. This class provides ways to modify its group of same objects.
                    /// </summary>
                    public interface IEmployed
                    {
                        int EmpID { get; set; }
                        string Name { get; set; }
                    }
                    /// <summary>
                    /// The 'Composite' class
                    /// </summary>
                    public class Employee : IEmployed, IEnumerable<IEmployed>
                    {
                        private List<IEmployed> _subordinates = new List<IEmployed>();

                        public int EmpID { get; set; }
                        public string Name { get; set; }

                        public void AddSubordinate(IEmployed subordinate)
                        {
                            _subordinates.Add(subordinate);
                        }

                        public void RemoveSubordinate(IEmployed subordinate)
                        {
                            _subordinates.Remove(subordinate);
                        }

                        public IEmployed GetSubordinate(int index)
                        {
                            return _subordinates[index];
                        }

                        public IEnumerator<IEmployed> GetEnumerator()
                        {
                            foreach (IEmployed subordinate in _subordinates)
                            {
                                yield return subordinate;
                            }
                        }

                        IEnumerator IEnumerable.GetEnumerator()
                        {
                            return GetEnumerator();
                        }
                    }
                    /// <summary>
                    /// The 'Leaf' class
                    /// </summary>
                    public class Contractor : IEmployed
                    {
                        public int EmpID { get; set; }
                        public string Name { get; set; }
                    }
                    /// <summary>
                    /// CompositeDesignSample
                    /// </summary>
                    public class CompositeDesignSample
                    {
                        public void GetCompositeDesignData()
                        {
                            Employee Rahul = new Employee { EmpID = 1, Name = "Rahul" };

                            Employee Amit = new Employee { EmpID = 2, Name = "Amit" };
                            Employee Mohan = new Employee { EmpID = 3, Name = "Mohan" };

                            Rahul.AddSubordinate(Amit);
                            Rahul.AddSubordinate(Mohan);

                            Employee Rita = new Employee { EmpID = 4, Name = "Rita" };
                            Employee Hari = new Employee { EmpID = 5, Name = "Hari" };

                            Amit.AddSubordinate(Rita);
                            Amit.AddSubordinate(Hari);

                            Employee Kamal = new Employee { EmpID = 6, Name = "Kamal" };
                            Employee Raj = new Employee { EmpID = 7, Name = "Raj" };

                            Contractor Sam = new Contractor { EmpID = 8, Name = "Sam" };
                            Contractor tim = new Contractor { EmpID = 9, Name = "Tim" };

                            Mohan.AddSubordinate(Kamal);
                            Mohan.AddSubordinate(Raj);
                            Mohan.AddSubordinate(Sam);
                            Mohan.AddSubordinate(tim);

                            Console.WriteLine("EmpID={0}, Name={1}", Rahul.EmpID, Rahul.Name);

                            foreach (Employee manager in Rahul)
                            {
                                Console.WriteLine("\n EmpID={0}, Name={1}", manager.EmpID, manager.Name);

                                foreach (var employee in manager)
                                {
                                    Console.WriteLine(" \t EmpID={0}, Name={1}", employee.EmpID, employee.Name);
                                }
                            }
                            Console.ReadKey();
                        }
                    }
                }
                namespace DecoratorDesignSample
                {
                    /// <summary>
                    /// The 'Decorator' pattern falls under Structural Pattern of Gang of Four (GOF) Design Patterns in .Net. Decorator pattern is used to add new functionality to an existing object without changing its structure. Hence Decorator pattern provides an alternative way to inheritance for modifying the behavior of an object. In this article, I would like share what is decorator pattern and how is it work?
                    /// What is Decorator Pattern
                    /// Decorator pattern is used to add new functionality to an existing object without changing its structure.
                    /// This pattern creates a decorator class which wraps the original class and add new behaviors/operations to an object at run-time.
                    /// </summary>
                    public interface Vehicle
                    {
                        string Make { get; }
                        string Model { get; }
                        double Price { get; }
                    }
                    /// <summary>
                    /// The 'ConcreteComponent' class
                    /// </summary>
                    public class HondaCity : Vehicle
                    {
                        public string Make
                        {
                            get { return "HondaCity"; }
                        }

                        public string Model
                        {
                            get { return "CNG"; }
                        }

                        public double Price
                        {
                            get { return 1000000; }
                        }
                    }
                    /// <summary>
                    /// The 'Decorator' abstract class
                    /// </summary>
                    public abstract class VehicleDecorator : Vehicle
                    {
                        private Vehicle _vehicle;

                        public VehicleDecorator(Vehicle vehicle)
                        {
                            _vehicle = vehicle;
                        }

                        public string Make
                        {
                            get { return _vehicle.Make; }
                        }

                        public string Model
                        {
                            get { return _vehicle.Model; }
                        }

                        public double Price
                        {
                            get { return _vehicle.Price; }
                        }

                    }
                    /// <summary>
                    /// The 'ConcreteDecorator' class
                    /// </summary>
                    public class SpecialOffer : VehicleDecorator
                    {
                        public SpecialOffer(Vehicle vehicle) : base(vehicle) { }

                        public int DiscountPercentage { get; set; }
                        public string Offer { get; set; }

                        public double Price
                        {
                            get
                            {
                                double price = base.Price;
                                int percentage = 100 - DiscountPercentage;
                                return Math.Round((price * percentage) / 100, 2);
                            }
                        }

                    }
                    /// <summary>
                    /// Decorator Pattern Demo
                    /// </summary>
                    public class DecoratorDesignSample
                    {
                        public void GetDecoratorDesignData()
                        {
                            // Basic vehicle
                            HondaCity car = new HondaCity();

                            Console.WriteLine("Honda City base price are : {0}", car.Price);

                            // Special offer
                            SpecialOffer offer = new SpecialOffer(car);
                            offer.DiscountPercentage = 25;
                            offer.Offer = "25 % discount";

                            Console.WriteLine("{1} @ Diwali Special Offer and price are : {0} ", offer.Price, offer.Offer);

                            Console.ReadKey();

                        }
                    }
                }
                namespace FacadeDesignSample
                {
                    /// <summary>
                    /// The 'Facade' pattern falls under Structural Pattern of Gang of Four (GOF) Design Patterns in .Net. The Facade design pattern is particularly used when a system is very complex or difficult to understand because system has a large number of interdependent classes or its source code is unavailable. In this article, I would like share what is facade pattern and how is it work?
                    /// What is facade Pattern
                    /// Facade pattern hides the complexities of the system and provides an interface to the client using which the client can access the system.
                    /// This pattern involves a single wrapper class which contains a set of members which are required by client. These members access the system on behalf of the facade client and hide the implementation details.
                    /// The facade design pattern is particularly used when a system is very complex or difficult to understand because system has a large number of interdependent classes or its source code is unavailable.
                    /// </summary>
                    public class CarModel
                    {
                        public void SetModel()
                        {
                            Console.WriteLine(" CarModel - SetModel");
                        }
                    }
                    /// <summary>
                    /// The 'Subsystem ClassB' class
                    /// </summary>
                    public class CarEngine
                    {
                        public void SetEngine()
                        {
                            Console.WriteLine(" CarEngine - SetEngine");
                        }
                    }
                    /// <summary>
                    /// The 'Subsystem ClassC' class
                    /// </summary>
                    public class CarBody
                    {
                        public void SetBody()
                        {
                            Console.WriteLine(" CarBody - SetBody");
                        }
                    }
                    /// <summary>
                    /// The 'Subsystem ClassD' class
                    /// </summary>
                    public class CarAccessories
                    {
                        public void SetAccessories()
                        {
                            Console.WriteLine(" CarAccessories - SetAccessories");
                        }
                    }
                    /// <summary>
                    /// The 'Facade' class
                    /// </summary>
                    public class CarFacade
                    {
                        CarModel model;
                        CarEngine engine;
                        CarBody body;
                        CarAccessories accessories;

                        public CarFacade()
                        {
                            model = new CarModel();
                            engine = new CarEngine();
                            body = new CarBody();
                            accessories = new CarAccessories();
                        }

                        public void CreateCompleteCar()
                        {
                            Console.WriteLine("******** Creating a Car **********\n");
                            model.SetModel();
                            engine.SetEngine();
                            body.SetBody();
                            accessories.SetAccessories();

                            Console.WriteLine("\n******** Car creation complete **********");
                        }
                    }
                    /// <summary>
                    /// Facade Pattern Demo
                    /// </summary>
                    public class FacadeDesignSample
                    {
                        public void GetFacadeDesignData()
                        {
                            CarFacade facade = new CarFacade();

                            facade.CreateCompleteCar();

                            Console.ReadKey();

                        }
                    }
                }
                namespace FlyweightDesignSample
                {
                    /// <summary>
                    /// The 'Flyweight' pattern falls under Structural Pattern of Gang of Four (GOF) Design Patterns in .Net. Flyweight pattern try to reuse already existing similar kind objects by storing them and creates new object when no matching object is found. In this article, I would like share what is flyweight pattern and how is it work?
                    /// What is Flyweight Pattern
                    /// Flyweight pattern is used to reduce the number of objects created, to decrease memory and resource usage. As a result it increase performance.
                    /// Flyweight pattern try to reuse already existing similar kind objects by storing them and creates new object when no matching object is found.
                    /// The flyweight pattern uses the concepts of intrinsic and extrinsic data.
                    /// Intrinsic data is held in the properties of the shared flyweight objects. This information is stateless and generally remains unchanged, if any change occurs it would be reflected among all of the objects that reference the flyweight.
                    /// Extrinsic data is computed on the fly means at runtime and it is held outside of a flyweight object. Hence it can be stateful.
                    /// </summary>
                    public interface IShape
                    {
                        void Print();
                    }
                    /// <summary>
                    /// A 'ConcreteFlyweight' class
                    /// </summary>
                    public class Rectangle : IShape
                    {
                        public void Print()
                        {
                            Console.WriteLine("Printing Rectangle");
                        }
                    }
                    /// <summary>
                    /// A 'ConcreteFlyweight' class
                    /// </summary>
                    public class Circle : IShape
                    {
                        public void Print()
                        {
                            Console.WriteLine("Printing Circle");
                        }
                    }
                    /// <summary>
                    /// The 'FlyweightFactory' class
                    /// </summary>
                    public class ShapeObjectFactory
                    {
                        Dictionary<string, IShape> shapes = new Dictionary<string, IShape>();

                        public int TotalObjectsCreated
                        {
                            get { return shapes.Count; }
                        }

                        public IShape GetShape(string ShapeName)
                        {
                            IShape shape = null;
                            if (shapes.ContainsKey(ShapeName))
                            {
                                shape = shapes[ShapeName];
                            }
                            else
                            {
                                switch (ShapeName)
                                {
                                    case "Rectangle":
                                        shape = new Rectangle();
                                        shapes.Add("Rectangle", shape);
                                        break;
                                    case "Circle":
                                        shape = new Circle();
                                        shapes.Add("Circle", shape);
                                        break;
                                    default:
                                        throw new Exception("Factory cannot create the object specified");
                                }
                            }
                            return shape;
                        }
                    }
                    /// <summary>
                    /// FlyweightDesignSample
                    /// </summary>
                    public class FlyweightDesignSample
                    {
                        public void GetFlyweightDesignData()
                        {
                            ShapeObjectFactory sof = new ShapeObjectFactory();

                            IShape shape = sof.GetShape("Rectangle");
                            shape.Print();
                            shape = sof.GetShape("Rectangle");
                            shape.Print();
                            shape = sof.GetShape("Rectangle");
                            shape.Print();

                            shape = sof.GetShape("Circle");
                            shape.Print();
                            shape = sof.GetShape("Circle");
                            shape.Print();
                            shape = sof.GetShape("Circle");
                            shape.Print();
                            int NumObjs = sof.TotalObjectsCreated;
                            Console.WriteLine("\nTotal No of Objects created = {0}", NumObjs);
                        }
                    }
                }
                namespace ProxySample
                {
                    public class ProxySample
                    {
                        /// <summary>
                        /// 
                        /// </summary>
                        public interface IClient
                        {
                            string GetData();
                        }
                        /// <summary>
                        /// The 'RealSubject' class
                        /// </summary>
                        public class RealClient : IClient
                        {
                            string Data;
                            public RealClient()
                            {
                                Console.WriteLine("Real Client: Initialized");
                                Data = "Dot Net Tricks";
                            }

                            public string GetData()
                            {
                                return Data;
                            }
                        }
                        /// <summary>
                        /// The 'Proxy Object' class
                        /// </summary>
                        public class ProxyClient : IClient
                        {
                            RealClient client = new RealClient();
                            public ProxyClient()
                            {
                                Console.WriteLine("ProxyClient: Initialized");
                            }

                            public string GetData()
                            {
                                return client.GetData();
                            }
                        }
                        /// <summary>
                        /// Proxy Pattern Demo
                        /// </summary>
                        public void GetProxyData()
                        {
                            ProxyClient proxy = new ProxyClient();
                            Console.WriteLine("Data from Proxy Client = {0}", proxy.GetData());
                        }
                    }
                }
            }
            ///BehavioralDesign
            namespace BehavioralDesign
            {
                namespace ChainofResponsibilityDesignSample
                {

                    /// <summary>
                    /// 'Chain' of Responsibility pattern falls under Behavioral Design Patterns of Gang of Four (GOF) Design Patterns in .Net. The chain of responsibility pattern is used to process a list or chain of various types of request and each of them may be handle by a different handler. In this article, I would like share what is chain of responsibility pattern and how is it work?
                    /// What is Chain of Responsibility Pattern
                    /// The chain of responsibility pattern is used to process a list or chain of various types of request and each of them may be handle by a different handler. This pattern decouples sender and receiver of a request based on type of request.
                    /// In this pattern, normally each receiver (handler) contains reference to another receiver. If one receiver cannot handle the request then it passes the same to the next receiver and so on.
                    /// </summary>
                    public class LoanEventArgs : EventArgs
                    {
                        internal Loan Loan { get; set; }
                    }
                    /// <summary>
                    /// The 'Handler' abstract class
                    /// </summary>
                    public abstract class Approver
                    {
                        // Loan event 
                        public EventHandler<LoanEventArgs> Loan;

                        // Loan event handler
                        public abstract void LoanHandler(object sender, LoanEventArgs e);

                        // Constructor
                        public Approver()
                        {
                            Loan += LoanHandler;
                        }

                        public void ProcessRequest(Loan loan)
                        {
                            OnLoan(new LoanEventArgs { Loan = loan });
                        }

                        // Invoke the Loan event
                        public virtual void OnLoan(LoanEventArgs e)
                        {
                            if (Loan != null)
                            {
                                Loan(this, e);
                            }
                        }

                        // Sets or gets the next approver
                        public Approver Successor { get; set; }
                    }
                    /// <summary>
                    /// The 'ConcreteHandler' class
                    /// </summary>
                    public class Clerk : Approver
                    {
                        public override void LoanHandler(object sender, LoanEventArgs e)
                        {
                            if (e.Loan.Amount < 25000.0)
                            {
                                Console.WriteLine("{0} approved request# {1}",
                                this.GetType().Name, e.Loan.Number);
                            }
                            else if (Successor != null)
                            {
                                Successor.LoanHandler(this, e);
                            }
                        }
                    }
                    /// <summary>
                    /// The 'ConcreteHandler' class
                    /// </summary>
                    public class AssistantManager : Approver
                    {
                        public override void LoanHandler(object sender, LoanEventArgs e)
                        {
                            if (e.Loan.Amount < 45000.0)
                            {
                                Console.WriteLine("{0} approved request# {1}",
                                this.GetType().Name, e.Loan.Number);
                            }
                            else if (Successor != null)
                            {
                                Successor.LoanHandler(this, e);
                            }
                        }
                    }
                    /// <summary>
                    /// The 'ConcreteHandler' clas
                    /// </summary>
                    public class Manager : Approver
                    {
                        public override void LoanHandler(object sender, LoanEventArgs e)
                        {
                            if (e.Loan.Amount < 100000.0)
                            {
                                Console.WriteLine("{0} approved request# {1}",
                                sender.GetType().Name, e.Loan.Number);
                            }
                            else if (Successor != null)
                            {
                                Successor.LoanHandler(this, e);
                            }
                            else
                            {
                                Console.WriteLine(
                                "Request# {0} requires an executive meeting!",
                                e.Loan.Number);
                            }
                        }
                    }
                    /// <summary>
                    /// Class that holds request details
                    /// </summary>
                    public class Loan
                    {
                        public double Amount { get; set; }
                        public string Purpose { get; set; }
                        public int Number { get; set; }
                    }
                    /// <summary>
                    /// ChainOfResponsibility Pattern Demo
                    /// </summary>
                    public class ChainofResponsibilityDesignSample
                    {
                        public void GetChainofResponsibilityDesignData()
                        {
                            // Setup Chain of Responsibility
                            Approver rohit = new Clerk();
                            Approver rahul = new AssistantManager();
                            Approver manoj = new Manager();

                            rohit.Successor = rahul;
                            rahul.Successor = manoj;

                            // Generate and process loan requests
                            var loan = new Loan { Number = 2034, Amount = 24000.00, Purpose = "Laptop Loan" };
                            rohit.ProcessRequest(loan);

                            loan = new Loan { Number = 2035, Amount = 42000.10, Purpose = "Bike Loan" };
                            rohit.ProcessRequest(loan);

                            loan = new Loan { Number = 2036, Amount = 156200.00, Purpose = "House Loan" };
                            rohit.ProcessRequest(loan);

                            // Wait for user
                        }
                    }
                }
                namespace ChainofResponsibilityDesignSample1
                {

                    /// <summary>
                    /// 'Chain' of Responsibility pattern falls under Behavioral Design Patterns of Gang of Four (GOF) Design Patterns in .Net. The chain of responsibility pattern is used to process a list or chain of various types of request and each of them may be handle by a different handler. In this article, I would like share what is chain of responsibility pattern and how is it work?
                    /// What is Chain of Responsibility Pattern
                    /// The chain of responsibility pattern is used to process a list or chain of various types of request and each of them may be handle by a different handler. This pattern decouples sender and receiver of a request based on type of request.
                    /// In this pattern, normally each receiver (handler) contains reference to another receiver. If one receiver cannot handle the request then it passes the same to the next receiver and so on.
                    /// </summary>
                    public enum BugType { Any, Feature, UI }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class BugHandler
                    {
                        private BugHandler m_oSuccessor;	//	in order to chain

                        public BugHandler(BugHandler o) { m_oSuccessor = o; }

                        virtual public void HandleBug(BugType t)
                        {
                            if (m_oSuccessor != null)
                            {
                                Console.WriteLine("...{0} passing to successor {1}",
                                    this.GetType().ToString(), m_oSuccessor.GetType().ToString());

                                m_oSuccessor.HandleBug(t);
                            }
                            else throw new Exception("Bug not handled!");
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class GenericBugHandler : BugHandler
                    {
                        public GenericBugHandler()
                            : base(null)
                        {
                            //	we're always last in the chain, so the bug stops here
                        }

                        override public void HandleBug(BugType t)
                        {
                            Console.WriteLine("-->  GenericBugHandler:  {0}", t.ToString());
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class FeatureBugHandler : BugHandler
                    {
                        public FeatureBugHandler(BugHandler o)
                            : base(o)
                        {
                        }

                        override public void HandleBug(BugType t)
                        {
                            if (BugType.Feature == t)
                                Console.WriteLine("-->  FeatureBugHandler:  {0}", t.ToString());
                            else base.HandleBug(t);	//	pass onto successor
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class UIBugHandler : BugHandler
                    {
                        public UIBugHandler(BugHandler o)
                            : base(o)
                        {
                        }

                        override public void HandleBug(BugType t)
                        {
                            if (BugType.UI == t)
                                Console.WriteLine("-->  UIBugHandler:  {0}", t.ToString());
                            else base.HandleBug(t);	//	pass onto successor
                        }
                    }
                    /// <summary>
                    /// ChainOfResponsibility Pattern Demo
                    /// </summary>
                    public class ChainofResponsibilityDesignSample
                    {
                        public void GetChainofResponsibilityDesignData()
                        {
                            //	build the chain
                            GenericBugHandler g = new GenericBugHandler();
                            FeatureBugHandler f1 = new FeatureBugHandler(g);
                            UIBugHandler chain1 = new UIBugHandler(f1);
                            UIBugHandler u = new UIBugHandler(g);
                            FeatureBugHandler chain2 = new FeatureBugHandler(u);

                            Console.WriteLine("Chain 1 UI:");
                            chain1.HandleBug(BugType.UI);
                            Console.WriteLine("Chain 2 UI:");
                            chain2.HandleBug(BugType.UI);
                            Console.WriteLine("Chain 1 Feature:");
                            chain1.HandleBug(BugType.Feature);
                            Console.WriteLine("Chain 2 Feature:");
                            chain2.HandleBug(BugType.Feature);
                            Console.WriteLine("Chain 1 Any:");
                            chain1.HandleBug(BugType.Any);
                            Console.WriteLine("Chain 2 Any:");
                            chain2.HandleBug(BugType.Any);
                            // Wait for user
                        }
                    }
                }
                namespace ChainofResponsibilityDesignSample2
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public class ChainofResponsibilityDesignSample
                    {
                        public void GetChainofResponsibilityDesignData()
                        {
                            // Setup Chain of Responsibility 
                            Handler h1 = new ConcreteHandler1();
                            Handler h2 = new ConcreteHandler2();
                            Handler h3 = new ConcreteHandler3();
                            h1.SetSuccessor(h2);
                            h2.SetSuccessor(h3);

                            // Generate and process request 
                            int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };

                            foreach (int request in requests)
                            {
                                h1.HandleRequest(request);
                            }

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// Handler
                    /// </summary>
                    public abstract class Handler
                    {
                        protected Handler successor;

                        public void SetSuccessor(Handler successor)
                        {
                            this.successor = successor;
                        }

                        public abstract void HandleRequest(int request);
                    }
                    /// <summary>
                    /// ConcreteHandler1
                    /// </summary>
                    public class ConcreteHandler1 : Handler
                    {
                        public override void HandleRequest(int request)
                        {
                            if (request >= 0 && request < 10)
                            {
                                Console.WriteLine("{0} handled request {1}",
                                  this.GetType().Name, request);
                            }
                            else if (successor != null)
                            {
                                successor.HandleRequest(request);
                            }
                        }
                    }
                    /// <summary>
                    /// ConcreteHandler2
                    /// </summary>
                    public class ConcreteHandler2 : Handler
                    {
                        public override void HandleRequest(int request)
                        {
                            if (request >= 10 && request < 20)
                            {
                                Console.WriteLine("{0} handled request {1}",
                                  this.GetType().Name, request);
                            }
                            else if (successor != null)
                            {
                                successor.HandleRequest(request);
                            }
                        }
                    }
                    /// <summary>
                    /// ConcreteHandler3
                    /// </summary>
                    public class ConcreteHandler3 : Handler
                    {
                        public override void HandleRequest(int request)
                        {
                            if (request >= 20 && request < 30)
                            {
                                Console.WriteLine("{0} handled request {1}",
                                  this.GetType().Name, request);
                            }
                            else if (successor != null)
                            {
                                successor.HandleRequest(request);
                            }
                        }
                    }
                }
                namespace CommandDesignSample
                {
                    /// <summary>
                    /// The 'Command' pattern falls under Behavioral Pattern of Gang of Four (GOF) Design Patterns in .Net. The command pattern is commonly used in the menu systems of many applications such as Editor, IDE etc. In this article, I would like share what is command pattern and how is it work?
                    /// What is Command Pattern
                    /// In this pattern, a request is wrapped under an object as a command and passed to invoker object. Invoker object pass the command to the appropriate object which can handle it and that object executes the command. This handles the request in traditional ways like as queuing and callbacks.
                    /// This pattern is commonly used in the menu systems of many applications such as Editor, IDE etc.
                    /// </summary>
                    public interface ICommand
                    {
                        void Execute();
                    }
                    /// <summary>
                    /// The 'Invoker' class
                    /// </summary>
                    public class Switch
                    {
                        private List<ICommand> _commands = new List<ICommand>();

                        public void StoreAndExecute(ICommand command)
                        {
                            _commands.Add(command);
                            command.Execute();
                        }
                    }
                    /// <summary>
                    /// The 'Receiver' class
                    /// </summary>
                    public class Light
                    {
                        public void TurnOn()
                        {
                            Console.WriteLine("The light is on");
                        }

                        public void TurnOff()
                        {
                            Console.WriteLine("The light is off");
                        }
                    }
                    /// <summary>
                    /// The Command for turning on the light - ConcreteCommand #1 
                    /// </summary>
                    public class FlipUpCommand : ICommand
                    {
                        private Light _light;

                        public FlipUpCommand(Light light)
                        {
                            _light = light;
                        }

                        public void Execute()
                        {
                            _light.TurnOn();
                        }
                    }
                    /// <summary>
                    /// The Command for turning off the light - ConcreteCommand #2 
                    /// </summary>
                    public class FlipDownCommand : ICommand
                    {
                        private Light _light;

                        public FlipDownCommand(Light light)
                        {
                            _light = light;
                        }

                        public void Execute()
                        {
                            _light.TurnOff();
                        }
                    }
                    /// <summary>
                    /// Command Pattern Demo
                    /// </summary>
                    public class CommandDesignSample
                    {
                        public void GetCommandDesignData()
                        {
                            Console.WriteLine("Enter Commands (ON/OFF) : ");
                            string cmd = Console.ReadLine();

                            Light lamp = new Light();
                            ICommand switchUp = new FlipUpCommand(lamp);
                            ICommand switchDown = new FlipDownCommand(lamp);

                            Switch s = new Switch();

                            if (cmd == "ON")
                            {
                                s.StoreAndExecute(switchUp);
                            }
                            else if (cmd == "OFF")
                            {
                                s.StoreAndExecute(switchDown);
                            }
                            else
                            {
                                Console.WriteLine("Command \"ON\" or \"OFF\" is required.");
                            }

                        }
                    }
                }
                namespace CommandDesignSample1
                {
                    /// <summary>
                    /// The 'Command' pattern falls under Behavioral Pattern of Gang of Four (GOF) Design Patterns in .Net. The command pattern is commonly used in the menu systems of many applications such as Editor, IDE etc. In this article, I would like share what is command pattern and how is it work?
                    /// What is Command Pattern
                    /// In this pattern, a request is wrapped under an object as a command and passed to invoker object. Invoker object pass the command to the appropriate object which can handle it and that object executes the command. This handles the request in traditional ways like as queuing and callbacks.
                    /// This pattern is commonly used in the menu systems of many applications such as Editor, IDE etc.
                    /// </summary>
                    public interface RadioCommand
                    {
                        void Execute(double f);
                        void Undo();
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class Radio
                    {
                        private string m_strName;	//	our radio name
                        private double m_fFreq;	//	our frequency

                        public Radio(string strName, double f)
                        {
                            m_strName = strName;
                            m_fFreq = f;
                        }

                        public double Frequency
                        {
                            get { return m_fFreq; }
                            set
                            {
                                m_fFreq = value;
                                Console.WriteLine("{0} radio freqency is now {1}",
                                    m_strName, m_fFreq);
                            }
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class ChangeFreqCommand : RadioCommand
                    {
                        private double m_fOld;	//	our old frequency
                        private Radio m_oRadio;	//	the radio we're concerned with

                        public ChangeFreqCommand(Radio o)
                        {
                            m_oRadio = o;
                            m_fOld = 0.0;
                        }

                        public void Execute(double f)
                        {
                            //	store the old frequency, then set to desired
                            m_fOld = m_oRadio.Frequency;
                            m_oRadio.Frequency = f;
                        }

                        public void Undo()
                        {
                            m_oRadio.Frequency = m_fOld;
                        }

                        /// <summary>
                        /// Command Pattern Demo
                        /// </summary>

                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class CommandDesignSample
                    {
                        public void GetCommandDesignData()
                        {
                            ArrayList cmds = new ArrayList();
                            Radio r = new Radio("Garmin COM", 121.5);	//	start on guard channel
                            ChangeFreqCommand c = new ChangeFreqCommand(r);

                            Console.WriteLine("Tuning...");

                            c.Execute(125.25);	//	get ATIS
                            cmds.Add(c);
                            c = new ChangeFreqCommand(r);
                            c.Execute(121.9);	//	tune to ground
                            cmds.Add(c);
                            c = new ChangeFreqCommand(r);
                            c.Execute(124.3);	//	tune to tower
                            cmds.Add(c);

                            Console.WriteLine("Undoing...");

                            cmds.Reverse();

                            foreach (ChangeFreqCommand x in cmds) x.Undo();

                        }
                    }
                }
                namespace CommandDesignSample2
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public class CommandDesignSample
                    {
                        public void GetCommandDesignData()
                        {
                            // Create receiver, command, and invoker 
                            Receiver receiver = new Receiver();
                            Command command = new ConcreteCommand(receiver);
                            Invoker invoker = new Invoker();

                            // Set and execute command 
                            invoker.SetCommand(command);
                            invoker.ExecuteCommand();

                            // Wait for user 
                            Console.Read();
                        }
                    }

                    // "Command" 
                    /// <summary>
                    /// 
                    /// </summary>
                    public abstract class Command
                    {
                        protected Receiver receiver;
                        // Constructor 
                        public Command(Receiver receiver)
                        {
                            this.receiver = receiver;
                        }
                        public abstract void Execute();
                    }

                    /// <summary>
                    /// ConcreteCommand
                    /// </summary>
                    public class ConcreteCommand : Command
                    {
                        // Constructor 
                        public ConcreteCommand(Receiver receiver) :
                            base(receiver)
                        {
                        }

                        public override void Execute()
                        {
                            receiver.Action();
                        }
                    }

                    /// <summary>
                    /// Receiver
                    /// </summary>
                    public class Receiver
                    {
                        public void Action()
                        {
                            Console.WriteLine("Called Receiver.Action()");
                        }
                    }

                    /// <summary>
                    /// Invoker
                    /// </summary>
                    public class Invoker
                    {
                        private Command command;

                        public void SetCommand(Command command)
                        {
                            this.command = command;
                        }

                        public void ExecuteCommand()
                        {
                            command.Execute();
                        }
                    }

                }
                namespace InterpreterSamle
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public interface AbstractExpression
                    {
                        void Interpret(ArrayList c);
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class NonTerminalExpression : AbstractExpression
                    {
                        public ArrayList expressions;	//	our contained expressions

                        public NonTerminalExpression()
                        {
                            expressions = new ArrayList();
                        }

                        public void Interpret(ArrayList c)
                        {
                            //	walk our contained expressions (possibly other non-terminals)
                            foreach (AbstractExpression e in expressions)
                                e.Interpret(c);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class IntegerExpression : AbstractExpression
                    {
                        public int literal;	//	storage for our literal

                        public IntegerExpression(int n)
                        {
                            literal = n;
                        }

                        public void Interpret(ArrayList c)
                        {
                            c.Add(literal);
                            Console.WriteLine("...pushed {0}", literal);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class AdditionTerminalExpression : AbstractExpression
                    {
                        public void Interpret(ArrayList c)
                        {
                            //	NOTE:	we can improve this with a better stack, and NOT
                            //		assume we're starting @ zero for any operation
                            int n = 0;

                            foreach (int i in c) n += i;	//	Pop off stack?

                            Console.WriteLine("==>  {0}", n);

                            c.Clear();	//	See note above, we'd rather pop each element in the loop above
                            c.Add(n);	//	push result back onto context
                            Console.WriteLine("...cleared stack, pushed {0}", n);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class RPNParser
                    {
                        public static void Parse(string s)
                        {
                            if (0 == s.Length) return;

                            Console.WriteLine("Parsing '{0}'", s);

                            //	the "root" of our syntax tree
                            NonTerminalExpression nte = new NonTerminalExpression();

                            //	tokenize
                            string[] tokens = s.Split(new char[] { ' ' });

                            foreach (string tok in tokens)
                            {
                                try
                                {
                                    //	attempt creation of an integer literal
                                    IntegerExpression ie = new IntegerExpression(Convert.ToInt32(tok));
                                    nte.expressions.Add(ie);

                                    continue;	//	next!
                                }
                                catch
                                {
                                    //	ignored, move to next type of expression
                                }

                                //	try addition
                                if ("+" == tok)
                                {
                                    nte.expressions.Add(new AdditionTerminalExpression());

                                    continue;
                                }

                                //	any other token is ignored
                            }

                            nte.Interpret(new ArrayList());
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class InterpreterSamle
                    {
                        public void GetInterpreterData()
                        {
                            RPNParser.Parse("4 5 +");
                            RPNParser.Parse("5 12 32 +");
                            RPNParser.Parse("55 32 + 29 441 +");
                        }
                    }
                }
                namespace IteratorSample
                {
                    /// <summary>
                    /// IteratorSample
                    /// </summary>
                    public class IteratorSample
                    {
                        public void GetIteratorData()
                        {
                            Context context = new Context();

                            // Usually a tree 
                            ArrayList list = new ArrayList();

                            // Populate 'abstract syntax tree' 
                            list.Add(new TerminalExpression());
                            list.Add(new NonterminalExpression());
                            list.Add(new TerminalExpression());
                            list.Add(new TerminalExpression());

                            // Interpret 
                            foreach (AbstractExpression exp in list)
                            {
                                exp.Interpret(context);
                            }

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// Context
                    /// </summary>
                    public class Context
                    {
                    }
                    /// <summary>
                    /// AbstractExpression
                    /// </summary>
                    public abstract class AbstractExpression
                    {
                        public abstract void Interpret(Context context);
                    }
                    /// <summary>
                    /// TerminalExpression
                    /// </summary>
                    public class TerminalExpression : AbstractExpression
                    {
                        public override void Interpret(Context context)
                        {
                            Console.WriteLine("Called Terminal.Interpret()");
                        }
                    }
                    /// <summary>
                    /// NonterminalExpression
                    /// </summary>
                    public class NonterminalExpression : AbstractExpression
                    {
                        public override void Interpret(Context context)
                        {
                            Console.WriteLine("Called Nonterminal.Interpret()");
                        }
                    }
                }
                namespace IteratorSample1
                {
                    /// <summary>
                    /// IteratorSample1
                    /// </summary>
                    public class IteratorSample
                    {
                        public void GetIteratorData()
                        {
                            ConcreteAggregate a = new ConcreteAggregate();
                            a[0] = "Item A";
                            a[1] = "Item B";
                            a[2] = "Item C";
                            a[3] = "Item D";

                            // Create Iterator and provide aggregate 
                            ConcreteIterator i = new ConcreteIterator(a);

                            Console.WriteLine("Iterating over collection:");

                            object item = i.First();
                            while (item != null)
                            {
                                Console.WriteLine(item);
                                item = i.Next();
                            }

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// Aggregate
                    /// </summary>
                    public abstract class Aggregate
                    {
                        public abstract Iterator CreateIterator();
                    }
                    /// <summary>
                    /// ConcreteAggregate
                    /// </summary>
                    public class ConcreteAggregate : Aggregate
                    {
                        private ArrayList items = new ArrayList();

                        public override Iterator CreateIterator()
                        {
                            return new ConcreteIterator(this);
                        }

                        // Property 
                        public int Count
                        {
                            get { return items.Count; }
                        }

                        // Indexer 
                        public object this[int index]
                        {
                            get { return items[index]; }
                            set { items.Insert(index, value); }
                        }
                    }
                    /// <summary>
                    /// Iterator
                    /// </summary>
                    public abstract class Iterator
                    {
                        public abstract object First();
                        public abstract object Next();
                        public abstract bool IsDone();
                        public abstract object CurrentItem();
                    }
                    /// <summary>
                    /// ConcreteIterator
                    /// </summary>
                    public class ConcreteIterator : Iterator
                    {
                        private ConcreteAggregate aggregate;
                        private int current = 0;

                        // Constructor 
                        public ConcreteIterator(ConcreteAggregate aggregate)
                        {
                            this.aggregate = aggregate;
                        }

                        public override object First()
                        {
                            return aggregate[0];
                        }

                        public override object Next()
                        {
                            object ret = null;
                            if (current < aggregate.Count - 1)
                            {
                                ret = aggregate[++current];
                            }

                            return ret;
                        }

                        public override object CurrentItem()
                        {
                            return aggregate[current];
                        }

                        public override bool IsDone()
                        {
                            return current >= aggregate.Count ? true : false;
                        }
                    }
                }
                namespace MediatorSample
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public class BaseRobot
                    {
                        protected RobotDirector m_Director;

                        public BaseRobot(RobotDirector d)
                        {
                            m_Director = d;
                        }

                        public void OperationalError(string reason)
                        {
                            m_Director.ErrorOccurred(this, reason);
                        }

                        public void Shutdown()
                        {
                            Console.WriteLine("...{0}, shutting down!", this.GetType().ToString());
                        }

                        virtual public void DoWork()
                        {
                            Console.WriteLine("{0}...doing work", this.GetType().ToString());
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class WelderRobot : BaseRobot
                    {
                        public WelderRobot(RobotDirector d) : base(d) { }

                        override public void DoWork()
                        {
                            base.DoWork();
                            OperationalError("O2 line breach!");
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class PainterRobot : BaseRobot
                    {
                        public PainterRobot(RobotDirector d) : base(d) { }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class AssemblyRobot : BaseRobot
                    {
                        public AssemblyRobot(RobotDirector d) : base(d) { }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class RobotDirector
                    {
                        protected WelderRobot m_Welder;
                        protected PainterRobot m_Painter;
                        protected AssemblyRobot m_Assembly;

                        public void Initialize()
                        {
                            m_Welder = new WelderRobot(this);
                            m_Painter = new PainterRobot(this);
                            m_Assembly = new AssemblyRobot(this);
                        }

                        public void DoWork()
                        {
                            m_Painter.DoWork();
                            m_Welder.DoWork();
                        }

                        public void ErrorOccurred(BaseRobot r, string why)
                        {
                            Console.WriteLine("Error Occurred from {0}:  {1}",
                                r.GetType().ToString(), why);

                            //	tell other robots to shut down
                            m_Welder.Shutdown();
                            m_Painter.Shutdown();
                            m_Assembly.Shutdown();
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class MediatorSample
                    {
                        public void GetMediatorData()
                        {
                            RobotDirector d = new RobotDirector();
                            d.Initialize();
                            d.DoWork();
                        }
                    }

                }
                namespace MediatorSample1
                {
                    /// <summary>
                    /// MediatorSample
                    /// </summary>
                    public class MediatorSample
                    {
                        public void GetMediatorData()
                        {
                            ConcreteMediator m = new ConcreteMediator();

                            ConcreteColleague1 c1 = new ConcreteColleague1(m);
                            ConcreteColleague2 c2 = new ConcreteColleague2(m);

                            m.Colleague1 = c1;
                            m.Colleague2 = c2;

                            c1.Send("How are you?");
                            c2.Send("Fine, thanks");

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// Mediator
                    /// </summary>
                    public abstract class Mediator
                    {
                        public abstract void Send(string message,
                          Colleague colleague);
                    }
                    /// <summary>
                    /// ConcreteMediator
                    /// </summary>
                    public class ConcreteMediator : Mediator
                    {
                        private ConcreteColleague1 colleague1;
                        private ConcreteColleague2 colleague2;

                        public ConcreteColleague1 Colleague1
                        {
                            set { colleague1 = value; }
                        }

                        public ConcreteColleague2 Colleague2
                        {
                            set { colleague2 = value; }
                        }

                        public override void Send(string message,
                          Colleague colleague)
                        {
                            if (colleague == colleague1)
                            {
                                colleague2.Notify(message);
                            }
                            else
                            {
                                colleague1.Notify(message);
                            }
                        }
                    }
                    /// <summary>
                    /// Colleague
                    /// </summary>
                    public abstract class Colleague
                    {
                        protected Mediator mediator;

                        // Constructor 
                        public Colleague(Mediator mediator)
                        {
                            this.mediator = mediator;
                        }
                    }
                    /// <summary>
                    /// ConcreteColleague1
                    /// </summary>
                    public class ConcreteColleague1 : Colleague
                    {
                        // Constructor 
                        public ConcreteColleague1(Mediator mediator)
                            : base(mediator)
                        {
                        }

                        public void Send(string message)
                        {
                            mediator.Send(message, this);
                        }

                        public void Notify(string message)
                        {
                            Console.WriteLine("Colleague1 gets message: "
                              + message);
                        }
                    }
                    /// <summary>
                    /// ConcreteColleague2
                    /// </summary>
                    public class ConcreteColleague2 : Colleague
                    {
                        // Constructor 
                        public ConcreteColleague2(Mediator mediator)
                            : base(mediator)
                        {
                        }

                        public void Send(string message)
                        {
                            mediator.Send(message, this);
                        }

                        public void Notify(string message)
                        {
                            Console.WriteLine("Colleague2 gets message: "
                              + message);
                        }
                    }

                }
                namespace MementoSample
                {
                    // MainApp startup class for Structural 
                    // Memento Design Pattern.
                    /// <summary>
                    /// MementoSample
                    /// </summary>
                    public class MementoSample
                    {
                        // Entry point into console application.
                        public void GetMementoData()
                        {
                            Originator o = new Originator();
                            o.State = "On";

                            // Store internal state
                            Caretaker c = new Caretaker();
                            c.Memento = o.CreateMemento();

                            // Continue changing originator
                            o.State = "Off";

                            // Restore saved state
                            o.SetMemento(c.Memento);

                            // Wait for user
                            Console.ReadKey();
                        }
                    }
                    /// <summary>
                    /// Originator
                    /// </summary>
                    public class Originator
                    {
                        private string _state;

                        // Property
                        public string State
                        {
                            get { return _state; }
                            set
                            {
                                _state = value;
                                Console.WriteLine("State = " + _state);
                            }
                        }

                        // Creates memento 
                        public Memento CreateMemento()
                        {
                            return (new Memento(_state));
                        }

                        // Restores original state
                        public void SetMemento(Memento memento)
                        {
                            Console.WriteLine("Restoring state...");
                            State = memento.State;
                        }
                    }
                    /// <summary>
                    /// Memento
                    /// </summary>
                    public class Memento
                    {
                        private string _state;

                        // Constructor
                        public Memento(string state)
                        {
                            this._state = state;
                        }

                        // Gets or sets state
                        public string State
                        {
                            get { return _state; }
                        }
                    }
                    /// <summary>
                    /// Caretaker
                    /// </summary>
                    public class Caretaker
                    {
                        private Memento _memento;

                        // Gets or sets memento
                        public Memento Memento
                        {
                            set { _memento = value; }
                            get { return _memento; }
                        }
                    }
                }
                namespace ObserverSample
                {
                    /// <summary>
                    /// PriceChanged
                    /// </summary>
                    /// <param name="p"></param>
                    public delegate void PriceChanged(BaseProduct p);
                    /// <summary>
                    /// 
                    /// </summary>
                    public class BaseProduct
                    {
                        private static int m_nIDPool = 1;
                        private int m_nID;
                        private string m_strName;
                        private double m_yPrice;

                        public event PriceChanged PriceChangedObservers;

                        public BaseProduct(string name, double price)
                        {
                            m_nID = m_nIDPool++;
                            m_strName = name;
                            m_yPrice = price;
                        }

                        public int id { get { return m_nID; } }

                        public string name
                        {
                            get { return m_strName; }
                            set { m_strName = value; }
                        }

                        public double price
                        {
                            get { return m_yPrice; }
                            set
                            {
                                m_yPrice = value;

                                //	notify listeners
                                PriceChangedObservers(this);
                            }
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class BaseObserver
                    {
                        virtual public void OnPriceChanged(BaseProduct p)
                        {
                            Console.WriteLine("{0} detected price change on {1}, ${2}",
                                this.GetType().ToString(), p.name, p.price);
                        }

                        public void ObserveProduct(BaseProduct p)
                        {
                            p.PriceChangedObservers += new PriceChanged(this.OnPriceChanged);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class POObserver : BaseObserver
                    {
                        override public void OnPriceChanged(BaseProduct p)
                        {
                            base.OnPriceChanged(p);
                            Console.WriteLine("{0} updating PO pricing", this.GetType().ToString());
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class UIObserver : BaseObserver
                    {
                        override public void OnPriceChanged(BaseProduct p)
                        {
                            base.OnPriceChanged(p);
                            Console.WriteLine("{0} updating UI view", this.GetType().ToString());
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class ObserverSample
                    {
                        public void GetObserverData()
                        {
                            BaseProduct p1 = new BaseProduct("Product A", 14.99);
                            BaseProduct p2 = new BaseProduct("Product B", 9.99);
                            UIObserver ui = new UIObserver();
                            POObserver po = new POObserver();

                            ui.ObserveProduct(p1);
                            ui.ObserveProduct(p2);
                            po.ObserveProduct(p1);
                            po.ObserveProduct(p2);

                            p1.price = 24.99;
                            p2.price = 11.99;
                        }
                    }

                }
                namespace ObserverSample1
                {
                    /// <summary>
                    /// ObserverSample1
                    /// </summary>
                    public class ObserverSample
                    {
                        public void GetObserverData()
                        {
                            // Configure Observer pattern 
                            ConcreteSubject s = new ConcreteSubject();

                            s.Attach(new ConcreteObserver(s, "X"));
                            s.Attach(new ConcreteObserver(s, "Y"));
                            s.Attach(new ConcreteObserver(s, "Z"));

                            // Change subject and notify observers 
                            s.SubjectState = "ABC";
                            s.Notify();

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// Subject
                    /// </summary>
                    public abstract class Subject
                    {
                        private ArrayList observers = new ArrayList();

                        public void Attach(Observer observer)
                        {
                            observers.Add(observer);
                        }

                        public void Detach(Observer observer)
                        {
                            observers.Remove(observer);
                        }

                        public void Notify()
                        {
                            foreach (Observer o in observers)
                            {
                                o.Update();
                            }
                        }
                    }
                    /// <summary>
                    /// ConcreteSubject
                    /// </summary>
                    public class ConcreteSubject : Subject
                    {
                        private string subjectState;

                        // Property 
                        public string SubjectState
                        {
                            get { return subjectState; }
                            set { subjectState = value; }
                        }
                    }
                    /// <summary>
                    /// Observer
                    /// </summary>
                    public abstract class Observer
                    {
                        public abstract void Update();
                    }
                    /// <summary>
                    /// ConcreteObserver
                    /// </summary>
                    public class ConcreteObserver : Observer
                    {
                        private string name;
                        private string observerState;
                        private ConcreteSubject subject;

                        // Constructor 
                        public ConcreteObserver(
                          ConcreteSubject subject, string name)
                        {
                            this.subject = subject;
                            this.name = name;
                        }

                        public override void Update()
                        {
                            observerState = subject.SubjectState;
                            Console.WriteLine("Observer {0}'s new state is {1}",
                              name, observerState);
                        }

                        // Property 
                        public ConcreteSubject Subject
                        {
                            get { return subject; }
                            set { subject = value; }
                        }
                    }
                }
                namespace StateSample
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public class OrderContext
                    {
                        private BaseOrderState m_State;
                        private int m_nOrderID;

                        public OrderContext(int id)
                        {
                            m_State = new OrderUnloadedState();
                            m_nOrderID = id;
                        }

                        public int OrderID { get { return m_nOrderID; } }

                        public void ChangeState(BaseOrderState s)
                        {
                            m_State = s;
                        }

                        public void Load() { m_State.Load(this, m_nOrderID); }

                        public double CalcSubtotal()
                        {
                            if (!m_State.IsLoaded()) Load();

                            return m_State.CalcSubtotal(this);
                        }

                        public double CalcTax()
                        {
                            if (!m_State.IsLoaded()) Load();

                            return m_State.CalcTax(this);
                        }

                        public double CalcTotal()
                        {
                            if (!m_State.IsLoaded()) Load();

                            return m_State.CalcTotal(this);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class BaseOrderState
                    {
                        protected void ChangeState(OrderContext c, BaseOrderState s)
                        {
                            c.ChangeState(s);
                        }

                        virtual public void Load(OrderContext c, int id) { }
                        virtual public bool IsLoaded() { return false; }
                        virtual public double CalcSubtotal(OrderContext c) { return 0; }
                        virtual public double CalcTax(OrderContext c) { return 0; }
                        virtual public double CalcTotal(OrderContext c) { return 0; }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class OrderUnloadedState : BaseOrderState
                    {
                        override public void Load(OrderContext c, int id)
                        {
                            OrderLoadedState s = new OrderLoadedState();

                            //	we would load the details from the DB at this point
                            Console.WriteLine("Loaded order details for order id {0}", id);

                            ChangeState(c, s);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class OrderLoadedState : BaseOrderState
                    {
                        //	we'd load these from a DB in real life
                        public const double TAX = 0.07;
                        public const double SUBTOTAL = 99.25;

                        override public void Load(OrderContext c, int id)
                        {
                            OrderLoadedState s = new OrderLoadedState();

                            //	we would load the details from the DB at this point
                            Console.WriteLine("Re-Loaded order details for order id {0}", id);

                            ChangeState(c, s);
                        }

                        override public bool IsLoaded() { return true; }

                        override public double CalcSubtotal(OrderContext c) { return SUBTOTAL; }
                        override public double CalcTax(OrderContext c) { return SUBTOTAL * TAX; }
                        override public double CalcTotal(OrderContext c) { return SUBTOTAL + (SUBTOTAL * TAX); }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class StateSample
                    {
                        public void GetStateData()
                        {
                            OrderContext c = new OrderContext(21);

                            Console.WriteLine("Order {0}...", c.OrderID);

                            double ySub = c.CalcSubtotal();
                            double yTax = c.CalcTax();
                            double yTotal = c.CalcTotal();

                            Console.WriteLine("Subtotal ${0}", ySub);
                            Console.WriteLine("Tax      ${0}", yTax);
                            Console.WriteLine("Total    ${0}", yTotal);
                        }
                    }
                }
                namespace StateSample1
                {
                    /// <summary>
                    /// StateSample1
                    /// </summary>
                    public class StateSample
                    {
                        public void GetStateData()
                        {
                            // Setup context in a state 
                            Context c = new Context(new ConcreteStateA());

                            // Issue requests, which toggles state 
                            c.Request();
                            c.Request();
                            c.Request();
                            c.Request();

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// State
                    /// </summary>
                    public abstract class State
                    {
                        public abstract void Handle(Context context);
                    }
                    /// <summary>
                    /// ConcreteStateA
                    /// </summary>
                    public class ConcreteStateA : State
                    {
                        public override void Handle(Context context)
                        {
                            context.State = new ConcreteStateB();
                        }
                    }
                    /// <summary>
                    /// ConcreteStateB
                    /// </summary>
                    public class ConcreteStateB : State
                    {
                        public override void Handle(Context context)
                        {
                            context.State = new ConcreteStateA();
                        }
                    }
                    /// <summary>
                    /// Context
                    /// </summary>
                    public class Context
                    {
                        private State state;

                        // Constructor 
                        public Context(State state)
                        {
                            this.State = state;
                        }

                        // Property 
                        public State State
                        {
                            get { return state; }
                            set
                            {
                                state = value;
                                Console.WriteLine("State: " +
                                  state.GetType().Name);
                            }
                        }

                        public void Request()
                        {
                            state.Handle(this);
                        }
                    }
                }
                namespace StrategySample
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public interface IOutputStrategy
                    {
                        void Output(ArrayList l);
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class UnsortedStrategy : IOutputStrategy
                    {
                        public void Output(ArrayList l)
                        {
                            Console.Write("(");

                            for (int i = 0; i < l.Count; i++)
                            {
                                Console.Write(l[i]);

                                if (i + 1 < l.Count) Console.Write(", ");
                            }

                            Console.WriteLine(")");
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class SortedStrategy : IOutputStrategy
                    {
                        public void Output(ArrayList l)
                        {
                            ArrayList s = new ArrayList(l);

                            s.Sort();

                            Console.Write("(");

                            for (int i = 0; i < s.Count; i++)
                            {
                                Console.Write(s[i]);

                                if (i + 1 < s.Count) Console.Write(", ");
                            }

                            Console.WriteLine(")");
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class ListContext
                    {
                        private ArrayList m_List;
                        private IOutputStrategy m_Strategy;

                        public ListContext(IOutputStrategy strategy)
                        {
                            m_Strategy = strategy;
                        }

                        public void SetStrategy(IOutputStrategy s)
                        {
                            m_Strategy = s;
                        }

                        public void MakeList(string s)
                        {
                            m_List = new ArrayList(s.Split(' '));
                        }

                        public void Output() { m_Strategy.Output(m_List); }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class StrategySample
                    {
                        public void GetStrategyData()
                        {
                            ListContext lc = new ListContext(new UnsortedStrategy());
                            lc.MakeList("This is a sample of varying the strategy");
                            lc.Output();
                            lc.SetStrategy(new SortedStrategy());
                            lc.Output();
                        }
                    }

                }
                namespace StrategySample1
                {
                    /// <summary>
                    /// StrategySample1
                    /// </summary>
                    public class StrategySample
                    {
                        public void GetStrategyData()
                        {
                            Context context;

                            // Three contexts following different strategies 
                            context = new Context(new ConcreteStrategyA());
                            context.ContextInterface();

                            context = new Context(new ConcreteStrategyB());
                            context.ContextInterface();

                            context = new Context(new ConcreteStrategyC());
                            context.ContextInterface();

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// Strategy
                    /// </summary>
                    public abstract class Strategy
                    {
                        public abstract void AlgorithmInterface();
                    }
                    /// <summary>
                    /// ConcreteStrategyA
                    /// </summary>
                    public class ConcreteStrategyA : Strategy
                    {
                        public override void AlgorithmInterface()
                        {
                            Console.WriteLine(
                              "Called ConcreteStrategyA.AlgorithmInterface()");
                        }
                    }
                    /// <summary>
                    /// ConcreteStrategyB
                    /// </summary>
                    public class ConcreteStrategyB : Strategy
                    {
                        public override void AlgorithmInterface()
                        {
                            Console.WriteLine(
                              "Called ConcreteStrategyB.AlgorithmInterface()");
                        }
                    }
                    /// <summary>
                    /// ConcreteStrategyC
                    /// </summary>
                    public class ConcreteStrategyC : Strategy
                    {
                        public override void AlgorithmInterface()
                        {
                            Console.WriteLine(
                              "Called ConcreteStrategyC.AlgorithmInterface()");
                        }
                    }
                    /// <summary>
                    /// Context
                    /// </summary>
                    public class Context
                    {
                        Strategy strategy;

                        // Constructor 
                        public Context(Strategy strategy)
                        {
                            this.strategy = strategy;
                        }

                        public void ContextInterface()
                        {
                            strategy.AlgorithmInterface();
                        }
                    }
                }
                namespace VisitorSample
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public class BaseSummingVisitor
                    {
                        protected int m_nNumProducts;
                        protected double m_ySum;

                        public BaseSummingVisitor()
                        {
                            m_nNumProducts = 0;
                            m_ySum = 0.0;
                        }

                        public void Reset()
                        {
                            m_nNumProducts = 0;
                            m_ySum = 0.0;
                        }

                        virtual public void VisitProduct(Product p) { }

                        virtual public void Results() { }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class Product
                    {
                        private string m_strName;
                        private int m_nOnHand;
                        private int m_nSoldToDate;
                        private double m_yCost;
                        private double m_yPrice;

                        public Product(string name, int onHand, int sold, double cost, double price)
                        {
                            m_strName = name;
                            m_nOnHand = onHand;
                            m_nSoldToDate = sold;
                            m_yCost = cost;
                            m_yPrice = price;
                        }

                        public string name { get { return m_strName; } }
                        public int onHand { get { return m_nOnHand; } }
                        public int sold { get { return m_nSoldToDate; } }
                        public double cost { get { return m_yCost; } }
                        public double price { get { return m_yPrice; } }

                        public void Accept(BaseSummingVisitor v)
                        {
                            v.VisitProduct(this);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class CostVisitor : BaseSummingVisitor
                    {
                        public CostVisitor() : base() { }

                        override public void VisitProduct(Product p)
                        {
                            m_nNumProducts += p.onHand;
                            m_ySum += (p.onHand * p.cost);
                        }

                        override public void Results()
                        {
                            Console.WriteLine("Total cost ({0} products):  ${1}", m_nNumProducts,
                                m_ySum);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class SalesVisitor : BaseSummingVisitor
                    {
                        public SalesVisitor() : base() { }

                        override public void VisitProduct(Product p)
                        {
                            m_nNumProducts += p.sold;
                            m_ySum += (p.sold * p.price);
                        }

                        override public void Results()
                        {
                            Console.WriteLine("Total sold ({0} products):  ${1}", m_nNumProducts,
                                m_ySum);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class PotentialSalesVisitor : BaseSummingVisitor
                    {
                        public PotentialSalesVisitor() : base() { }

                        override public void VisitProduct(Product p)
                        {
                            m_nNumProducts += p.onHand;
                            m_ySum += (p.onHand * p.price);
                        }

                        override public void Results()
                        {
                            Console.WriteLine("Total potential sales ({0} products):  ${1}", m_nNumProducts,
                                m_ySum);
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class VisitorSample
                    {
                        public void GetVisitorData()
                        {
                            ArrayList l = new ArrayList();

                            l.Add(new Product("chair", 5, 2, 10.00, 24.99));
                            l.Add(new Product("desk", 10, 5, 45.00, 150.00));
                            l.Add(new Product("filing cabinet", 20, 7, 15.00, 45.00));

                            CostVisitor cv = new CostVisitor();
                            SalesVisitor sv = new SalesVisitor();
                            PotentialSalesVisitor psv = new PotentialSalesVisitor();

                            foreach (Product p in l)
                            {
                                p.Accept(cv);
                                p.Accept(sv);
                                p.Accept(psv);
                            }

                            cv.Results();
                            psv.Results();
                            sv.Results();
                        }
                    }

                }
                namespace VisitorSample1
                {
                    /// <summary>
                    /// VisitorSample1
                    /// </summary>
                    public class VisitorSample
                    {
                        public void GetVisitorData()
                        {
                            // Setup structure 
                            ObjectStructure o = new ObjectStructure();
                            o.Attach(new ConcreteElementA());
                            o.Attach(new ConcreteElementB());

                            // Create visitor objects 
                            ConcreteVisitor1 v1 = new ConcreteVisitor1();
                            ConcreteVisitor2 v2 = new ConcreteVisitor2();

                            // Structure accepting visitors 
                            o.Accept(v1);
                            o.Accept(v2);

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// Visitor
                    /// </summary>
                    public abstract class Visitor
                    {
                        public abstract void VisitConcreteElementA(
                          ConcreteElementA concreteElementA);
                        public abstract void VisitConcreteElementB(
                          ConcreteElementB concreteElementB);
                    }
                    /// <summary>
                    /// ConcreteVisitor1
                    /// </summary>
                    public class ConcreteVisitor1 : Visitor
                    {
                        public override void VisitConcreteElementA(
                          ConcreteElementA concreteElementA)
                        {
                            Console.WriteLine("{0} visited by {1}",
                              concreteElementA.GetType().Name, this.GetType().Name);
                        }

                        public override void VisitConcreteElementB(
                          ConcreteElementB concreteElementB)
                        {
                            Console.WriteLine("{0} visited by {1}",
                              concreteElementB.GetType().Name, this.GetType().Name);
                        }
                    }
                    /// <summary>
                    /// ConcreteVisitor2
                    /// </summary>
                    public class ConcreteVisitor2 : Visitor
                    {
                        public override void VisitConcreteElementA(
                          ConcreteElementA concreteElementA)
                        {
                            Console.WriteLine("{0} visited by {1}",
                              concreteElementA.GetType().Name, this.GetType().Name);
                        }

                        public override void VisitConcreteElementB(
                          ConcreteElementB concreteElementB)
                        {
                            Console.WriteLine("{0} visited by {1}",
                              concreteElementB.GetType().Name, this.GetType().Name);
                        }
                    }
                    /// <summary>
                    /// Element
                    /// </summary>
                    public abstract class Element
                    {
                        public abstract void Accept(Visitor visitor);
                    }
                    /// <summary>
                    /// ConcreteElementA
                    /// </summary>
                    public class ConcreteElementA : Element
                    {
                        public override void Accept(Visitor visitor)
                        {
                            visitor.VisitConcreteElementA(this);
                        }

                        public void OperationA()
                        {
                        }
                    }
                    /// <summary>
                    /// ConcreteElementB
                    /// </summary>
                    public class ConcreteElementB : Element
                    {
                        public override void Accept(Visitor visitor)
                        {
                            visitor.VisitConcreteElementB(this);
                        }

                        public void OperationB()
                        {
                        }
                    }
                    /// <summary>
                    /// ObjectStructure
                    /// </summary>
                    public class ObjectStructure
                    {
                        private ArrayList elements = new ArrayList();

                        public void Attach(Element element)
                        {
                            elements.Add(element);
                        }

                        public void Detach(Element element)
                        {
                            elements.Remove(element);
                        }

                        public void Accept(Visitor visitor)
                        {
                            foreach (Element e in elements)
                            {
                                e.Accept(visitor);
                            }
                        }
                    }
                }
                namespace TemplateMethodSample
                {
                    /// <summary>
                    /// TemplateMethodSample
                    /// </summary>
                    public class TemplateMethodSample
                    {
                        public void GetTemplateMethodData()
                        {
                            AbstractClass c;

                            c = new ConcreteClassA();
                            c.TemplateMethod();

                            c = new ConcreteClassB();
                            c.TemplateMethod();

                            // Wait for user 
                            Console.Read();
                        }
                    }
                    /// <summary>
                    /// AbstractClass
                    /// </summary>
                    public abstract class AbstractClass
                    {
                        public abstract void PrimitiveOperation1();
                        public abstract void PrimitiveOperation2();

                        // The "Template method" 
                        public void TemplateMethod()
                        {
                            PrimitiveOperation1();
                            PrimitiveOperation2();
                            Console.WriteLine("");
                        }
                    }
                    /// <summary>
                    /// ConcreteClass
                    /// </summary>
                    public class ConcreteClassA : AbstractClass
                    {
                        public override void PrimitiveOperation1()
                        {
                            Console.WriteLine("ConcreteClassA.PrimitiveOperation1()");
                        }
                        public override void PrimitiveOperation2()
                        {
                            Console.WriteLine("ConcreteClassA.PrimitiveOperation2()");
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class ConcreteClassB : AbstractClass
                    {
                        public override void PrimitiveOperation1()
                        {
                            Console.WriteLine("ConcreteClassB.PrimitiveOperation1()");
                        }
                        public override void PrimitiveOperation2()
                        {
                            Console.WriteLine("ConcreteClassB.PrimitiveOperation2()");
                        }
                    }
                }
            }
            ///GenericsCollections
            namespace GenericsCollections
            {
                namespace Generics
                {
                    public class Generics
                    {
                        public Generics()
                        {
                            //GenericDelegates();
                        }


                        #region Lambda Expressions

                        public static void GenericDelegates()
                        {
                            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
                            Func<int, bool> where = n => n < 6;
                            Func<int, int> select = n => n;
                            Func<int, string> orderby = n => n % 2 == 0 ? "even" : "odd";
                            var nums = numbers.Where(where).OrderBy(orderby).Select(select);
                            Console.Write(nums);
                            Test.GetMain();
                        }
                        delegate bool D();
                        delegate bool D2(int i);
                        public class Test
                        {
                            D del;
                            D2 del2;
                            public void TestMethod(int input)
                            {
                                int j = 0;
                                // Initialize the delegates with lambda expressions.
                                // Note access to 2 outer variables.
                                // del will be invoked within this method.
                                del = () => { j = 10; return j > input; };

                                // del2 will be invoked after TestMethod goes out of scope.
                                del2 = (x) => { return x == j; };

                                // Demonstrate value of j:
                                // Output: j = 0 
                                // The delegate has not been invoked yet.
                                Console.WriteLine("j = {0}", j);        // Invoke the delegate.
                                bool boolResult = del();

                                // Output: j = 10 b = True
                                Console.WriteLine("j = {0}. b = {1}", j, boolResult);
                            }

                            public static void GetMain()
                            {
                                Test test = new Test();
                                test.TestMethod(5);

                                // Prove that del2 still has a copy of
                                // local variable j from TestMethod.
                                bool result = test.del2(10);
                                // Output: True
                                Console.WriteLine(result);
                            }
                        }

                        #endregion Lambda Expressions

                    }
                }
            }
            ///OOPS
            namespace OOPS
            {
                public class OOPSHelp
                {

                    public OOPSHelp()
                    {
                    }
                }
                namespace OOPSSamples
                {

                    public class OOPSClass
                    {
                        public OOPSClass()
                        {
                            GetInheritanceConcept();
                        }
                        private void GetInheritanceConcept()
                        {
                            BaseClass BC = new BaseClass();

                            BC.AMethod();
                            BC.AMethod(5);
                            BC.AMethod(5, 10);
                            BC.AMethod(5, 10, 15);




                            DerivedClass DRVC = new DerivedClass();
                            DRVC.AMethod();
                            DRVC.AMethod(5);
                            DRVC.AMethod(5, 10);
                            DRVC.AMethod(5, 10, 15);
                            DRVC.GetExtentions();
                            DRVC.GetOptionalParametersImplementer();
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class BaseClass
                    {
                        public int MyA { get; set; }
                        public int MyB { get; set; }

                        internal int MyC { get; set; }
                        internal int MyD { get; set; }

                        protected internal int MyE { get; set; }
                        protected internal int MyF { get; set; }

                        protected int MyG { get; set; }
                        protected int MyH { get; set; }

                        public BaseClass()
                        {

                        }
                        public BaseClass(int A)
                        {
                        }
                        public BaseClass(int A, int B)
                        {
                        }
                        public virtual void AMethod()
                        {
                        }
                        public virtual void AMethod(int A)
                        {
                        }
                        public virtual void AMethod(int A, int B)
                        {
                        }
                        public void AMethod(int A, int B, int C)
                        {
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class DerivedClass : BaseClass
                    {
                        public DerivedClass()
                            : base(5)
                        {

                        }
                        public DerivedClass(int A)
                            : base(5, 10) // call base constructor BaseClass(10)
                        {
                        }
                        public DerivedClass(int A, int B)
                            : this(51)// call DerivedClass()
                        {
                        }
                        public override void AMethod()
                        {
                            base.AMethod();
                        }
                        public override void AMethod(int A)
                        {
                            base.AMethod(A);
                        }
                        public new void AMethod(int A, int B)//Heiding Methods
                        {
                        }
                        public void AMethod(int A, int B, int C)//Heiding Methods but it will give the worning
                        {
                        }

                        public void GetExtentions()
                        {
                            string s = "Hello Extension Methods,Hello.Extension?Methods";
                            int i = s.WordCount();


                            // Declare an instance of class A, class B, and class C.
                            A a = new A();
                            B b = new B();
                            C c = new C();

                            // For a, b, and c, call the following methods: 
                            //      -- MethodA with an int argument 
                            //      -- MethodA with a string argument 
                            //      -- MethodB with no argument. 

                            // A contains no MethodA, so each call to MethodA resolves to  
                            // the extension method that has a matching signature.
                            a.MethodA(1);           // Extension.MethodA(object, int)
                            a.MethodA("hello");     // Extension.MethodA(object, string) 

                            // A has a method that matches the signature of the following call 
                            // to MethodB.
                            a.MethodB();            // A.MethodB() 

                            // B has methods that match the signatures of the following 
                            // nethod calls.
                            b.MethodA(1);           // B.MethodA(int)
                            b.MethodB();            // B.MethodB() 

                            // B has no matching method for the following call, but  
                            // class Extension does.
                            b.MethodA("hello");     // Extension.MethodA(object, string) 

                            // C contains an instance method that matches each of the following 
                            // method calls.
                            c.MethodA(1);           // C.MethodA(object)
                            c.MethodA("hello");     // C.MethodA(object)
                            c.MethodB();
                        }

                        public void GetGeneric()
                        {
                            SampleGeneric<int> list1 = new SampleGeneric<int>();
                            list1.Field = 100;
                            // Declare a list of type string.
                            SampleGeneric<string> list2 = new SampleGeneric<string>();
                            list2.Field = "Test Data";
                            // Declare a list of type ExampleClass.
                            SampleGeneric<SampleClass> list3 = new SampleGeneric<SampleClass>();
                            list3.Field = new SampleClass();
                        }

                        public void GetOptionalParametersImplementer()
                        {
                            OptionalParametersImplementer OPI = new OptionalParametersImplementer();
                            OPI.Add(5, 0);
                            OPI.Delete(5, 2);
                            OPI.OptionalParametersCalculateWage(15);
                            OPI.OptionalParametersCalculateWage(15, 2);
                            OPI.OptionalParametersCalculateWage(15);

                            OPI.NamedParametersCalculateWage(B: 10, E: 5, D: 2, A: 15, C: 25);


                        }

                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public static class MyExtensions
                    {
                        public static int WordCount(this String str)
                        {
                            return str.Split(new char[] { ' ', '.', ',', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public interface IMyInterface
                    {
                        // Any class that implements IMyInterface must define a method 
                        // that matches the following signature. 
                        void MethodB();
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public static class Extension
                    {
                        public static void MethodA(this IMyInterface myInterface, int i)
                        {
                            Console.WriteLine("Extension.MethodA(this IMyInterface myInterface, int i)");
                        }

                        public static void MethodA(this IMyInterface myInterface, string s)
                        {
                            Console.WriteLine("Extension.MethodA(this IMyInterface myInterface, string s)");
                        }

                        // This method is never called in ExtensionMethodsDemo1, because each  
                        // of the three classes A, B, and C implements a method named MethodB 
                        // that has a matching signature. 
                        public static void MethodB(this IMyInterface myInterface)
                        {
                            Console.WriteLine("Extension.MethodB(this IMyInterface myInterface)");
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class A : IMyInterface
                    {
                        public void MethodB() { Console.WriteLine("A.MethodB()"); }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class B : IMyInterface
                    {
                        public void MethodB() { Console.WriteLine("B.MethodB()"); }
                        public void MethodA(int i) { Console.WriteLine("B.MethodA(int i)"); }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class C : IMyInterface
                    {
                        public void MethodB() { Console.WriteLine("C.MethodB()"); }
                        public void MethodA(object obj)
                        {
                            Console.WriteLine("C.MethodA(object obj)");
                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    /// <typeparam name="T"></typeparam>
                    public class SampleGeneric<T>
                    {
                        public T Field;
                        public void Add(T input) { }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class SampleClass
                    {
                        public SampleClass()
                        {

                        }
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public interface IMyOptionalParametersInterface
                    {
                        String Update(int A, int? B);
                        String Add(int A, int? B);
                        String Delete(int A, int? B);
                        String Search(int A, int B = 10);
                        String OptionalParametersCalculateWage(int A, int B = 10);
                        String OptionalParametersCalculateWage(int A, int? B, string C = "0");
                        String OptionalParametersCalculateWage(int A, int? B, int C = 0, string D = "0");
                        String OptionalParametersCalculateWage(int A, int? B, int C = 0, string D = "0", double E = 0.0);
                        String OptionalParametersCalculateWage(int A, int? B, int C = 0, string D = "0", double E = 0.0, int? F = null);
                        String NamedParametersCalculateWage(int A, int B, int C, int D, int E);
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public interface IMyBaseOptionalParametersInterface : IMyOptionalParametersInterface
                    {


                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    public class OptionalParametersImplementer : IMyOptionalParametersInterface
                    {

                        public string Update(int A, int? B)
                        {
                            return string.Empty;
                        }

                        public string Add(int A, int? B)
                        {
                            return string.Empty;
                        }

                        public string Delete(int A, int? B)
                        {
                            return string.Empty;
                        }

                        public string Search(int A, int B)
                        {
                            return string.Empty;
                        }

                        public string OptionalParametersCalculateWage(int A, int B = 0)
                        {
                            return string.Empty;
                        }

                        public string OptionalParametersCalculateWage(int A, int? B, string C = "0")
                        {
                            return string.Empty;
                        }

                        public string OptionalParametersCalculateWage(int A, int? B, int C = 0, string D = "0")
                        {
                            return string.Empty;
                        }

                        public string OptionalParametersCalculateWage(int A, int? B, int C = 0, string D = "0", double E = 0.0)
                        {
                            return string.Empty;
                        }

                        public string NamedParametersCalculateWage(int A, int B, int C, int D, int E)
                        {
                            return Convert.ToString(A + B + C + D + E);
                        }


                        public string OptionalParametersCalculateWage(int A, int? B, int C = 0, string D = "0", double E = 0.0, int? F = null)
                        {
                            return Convert.ToString(A + B + C + D + E);
                        }
                    }

                }
                namespace PolymorphismSample
                {
                    public class PolimorphismPrimary
                    {
                        public PolimorphismPrimary()
                        {
                            //Main();
                            ///Contructor here
                        }

                        public void GetPolimorphismPrimaryData()
                        {
                            PMLAnimal a1 = new PMLAnimal();
                            a1.Talk();
                            a1.Sing();
                            a1.Greet();

                            Console.WriteLine();

                            PMLAnimal a2 = new PMLDog();
                            a2.Talk();
                            a2.Sing();
                            a2.Greet();

                            Console.WriteLine();

                            PMLDog d1 = new PMLDog();
                            d1.Talk();
                            d1.Sing();
                            d1.Greet();

                            Console.WriteLine();

                            PMLColor c1 = new PMLColor();
                            c1.Fill();
                            c1.Fill("red");

                            Console.WriteLine();

                            PMLGreen g1 = new PMLGreen();
                            g1.Fill();
                            g1.Fill("violet");

                            Console.WriteLine();

                            PMLMicrosoftSoftware m1 = new PMLMicrosoftSoftware();
                            //MicrosoftSoftware m2 = new MicrosoftSoftware(300); //won't compile
                            Console.WriteLine();
                            PMLDundasSoftware du1 = new PMLDundasSoftware(50);
                            Console.WriteLine();
                            PMLDundasSoftware du2 = new PMLDundasSoftware("test", 75);
                        }
                    }

                    public class PMLAnimal
                    {
                        public PMLAnimal()
                        {
                            HttpContext.Current.Response.Write("Animal constructor <br>");
                            //Console.WriteLine("Animal constructor");
                        }
                        public void Greet()
                        {
                            HttpContext.Current.Response.Write("Animal says Hello <br>");
                            //Console.WriteLine("Animal says Hello");
                        }
                        public void Talk()
                        {
                            HttpContext.Current.Response.Write("Animal talk <br>");
                            //Console.WriteLine("Animal talk");
                        }
                        public virtual void Sing()
                        {
                            HttpContext.Current.Response.Write("Animal song <br>");
                            //Console.WriteLine("Animal song");
                        }
                    };

                    public class PMLDog : PMLAnimal
                    {
                        public PMLDog()
                        {
                            HttpContext.Current.Response.Write("Dog constructor <br>");
                            //Console.WriteLine("Dog constructor");
                        }
                        public new void Talk()
                        {
                            HttpContext.Current.Response.Write("Dog talk <br>");
                            //Console.WriteLine("Dog talk");
                        }
                        public override void Sing()
                        {
                            HttpContext.Current.Response.Write("Dog song <br>");
                            //Console.WriteLine("Dog song");
                        }
                    };

                    public class PMLColor
                    {
                        public virtual void Fill()
                        {
                            HttpContext.Current.Response.Write("Fill me up with color <br>");
                            //Console.WriteLine("Fill me up with color");
                        }
                        public void Fill(string s)
                        {
                            HttpContext.Current.Response.Write(s + "<br>");
                            //Console.WriteLine("Fill me up with {0}",s);
                        }
                    };

                    public class PMLGreen : PMLColor
                    {
                        public override void Fill()
                        {
                            HttpContext.Current.Response.Write("Fill me up with green <br>");
                            //Console.WriteLine("Fill me up with green");
                        }
                    };

                    public class PMLSoftware
                    {
                        public PMLSoftware()
                        {
                            m_x = 100;
                        }
                        public PMLSoftware(int y)
                        {
                            m_x = y;
                        }
                        protected int m_x;
                    };

                    public class PMLMicrosoftSoftware : PMLSoftware
                    {
                        public PMLMicrosoftSoftware()
                        {
                            HttpContext.Current.Response.Write(m_x + "<br>");
                            //Console.WriteLine(m_x);
                        }
                    };

                    public class PMLDundasSoftware : PMLSoftware
                    {
                        public PMLDundasSoftware(int y)
                            : base(y)
                        {
                            HttpContext.Current.Response.Write(m_x + "<br>");
                            //Console.WriteLine(m_x);
                        }
                        public PMLDundasSoftware(string s, int f)
                            : this(f)
                        {
                            HttpContext.Current.Response.Write(s + "<br>");
                            //Console.WriteLine(s);
                        }
                    };
                }
                namespace PolymorphismSample1
                {
                    public class BaseClass
                    {
                        public void DoWork() { }
                        public int WorkField;
                        public int WorkProperty
                        {
                            get { return 0; }
                        }
                    }

                    public class DerivedClass : BaseClass
                    {
                        public new void DoWork() { }
                        public new int WorkField;
                        public new int WorkProperty
                        {
                            get { return 0; }
                        }
                    }

                    public class MyPolyClass
                    {
                        public MyPolyClass()
                        {

                        }
                        public void GetMainData()
                        {
                            DerivedClass B = new DerivedClass();
                            B.DoWork();  // Calls the new method.
                            BaseClass A = (BaseClass)B;
                            A.DoWork();  // Calls the old method.
                        }
                    }

                    public class A
                    {
                        public virtual void DoWork() { }
                        public virtual void DoSomeWork() { }
                    }

                    public class B : A
                    {
                        public override void DoWork() { }
                        public override void DoSomeWork() { }
                    }

                    public class C : B
                    {
                        public override void DoWork() { }
                        public sealed override void DoSomeWork() { }
                    }
                    public class D : C
                    {
                        public override void DoWork() { }
                        public new void DoSomeWork() { }
                    }
                    public class E : D
                    {
                        public override void DoWork()
                        {
                            base.DoWork();
                        }

                    }
                }
                namespace AbstractsVSInterfaces
                {
                    public interface IParentInterface
                    {
                        String ID { get; set; }
                        String FirstName { get; set; }
                        String LastName { get; set; }
                        String Update();
                        String Add();
                        String Delete();
                        String Search();
                        String CalculateWage();
                    }
                    public interface IMyChieldInterface : IParentInterface
                    {
                        String Update(int A);
                        String Add(int A);
                        String Delete(int A);
                        String Search(int A);
                        String CalculateWage(int A);
                    }
                    public interface IMyInterface : IMyChieldInterface
                    {
                        String Update(int A, int B);
                        String Add(int A, int B);
                        String Delete(int A, int B);
                        String Search(int A, int B);
                        String CalculateWage(int A, int B);
                    }
                    public interface IMyBaseInterface : IMyInterface
                    {


                    }
                    public class InterfaceImplementer : IMyBaseInterface
                    {

                        public string ID
                        { get; set; }
                        public string FirstName
                        { get; set; }
                        public string LastName
                        { get; set; }

                        public string Update()
                        {
                            return string.Empty;
                        }

                        public string Add()
                        {
                            return string.Empty;
                        }

                        public string Delete()
                        {
                            return string.Empty;
                        }

                        public string Search()
                        {
                            return string.Empty;
                        }

                        public string CalculateWage()
                        {
                            return string.Empty;
                        }
                        public string Update(int A)
                        {
                            return string.Empty;
                        }

                        public string Add(int A)
                        {
                            return string.Empty;
                        }

                        public string Delete(int A)
                        {
                            return string.Empty;
                        }

                        public string Search(int A)
                        {
                            return string.Empty;
                        }

                        public string CalculateWage(int A)
                        {
                            return string.Empty;
                        }


                        public string Update(int A, int B)
                        {
                            return string.Empty;
                        }

                        public string Add(int A, int B)
                        {
                            return string.Empty;
                        }

                        public string Delete(int A, int B)
                        {
                            return string.Empty;
                        }

                        public string Search(int A, int B)
                        {
                            return string.Empty;
                        }

                        public string CalculateWage(int A, int B)
                        {
                            return string.Empty;
                        }
                    }

                    public abstract class Parentabstract : IMyBaseInterface
                    {
                        public Parentabstract()
                        {

                        }
                        public abstract string Name { get; set; }
                        public abstract string Address { get; set; }
                        public abstract string Phone { get; set; }
                        public abstract string AbAdd();
                        public abstract string AbAdd(int A);
                        public abstract string AbAdd(int A, int B);
                        public abstract void AbAdd(int A, int B, int C);
                        public abstract float Area();
                        public abstract float Circumference();



                        public string ID { get; set; }
                        public string FirstName { get; set; }
                        public string LastName { get; set; }
                        public string Update()
                        {
                            return string.Empty;
                        }
                        public string Add()
                        {
                            return string.Empty;
                        }
                        public string Delete()
                        {
                            return string.Empty;
                        }
                        public string Search()
                        {
                            return string.Empty;
                        }
                        public string CalculateWage()
                        {
                            return string.Empty;
                        }
                        public string Update(int A, int B)
                        {
                            return string.Empty;
                        }
                        public string Update(int A)
                        {
                            return string.Empty;
                        }
                        public string Add(int A)
                        {
                            return string.Empty;
                        }
                        public string Delete(int A)
                        {
                            return string.Empty;
                        }
                        public string Search(int A)
                        {
                            return string.Empty;
                        }
                        public string CalculateWage(int A)
                        {
                            return string.Empty;
                        }
                        public string Add(int A, int B)
                        {
                            return string.Empty;
                        }
                        public string Delete(int A, int B)
                        {
                            return string.Empty;
                        }
                        public string Search(int A, int B)
                        {
                            return string.Empty;
                        }
                        public string CalculateWage(int A, int B)
                        {
                            return string.Empty;
                        }
                    }
                    public abstract class BaseParentabstract : Parentabstract
                    {
                        public BaseParentabstract()
                        {

                        }


                    }
                    public class AbstractImplementer : BaseParentabstract
                    {
                        public AbstractImplementer()
                        {

                        }
                        public override string Name { get; set; }
                        public override string Address { get; set; }
                        public override string Phone { get; set; }
                        public override string AbAdd()
                        {
                            return string.Empty;
                        }
                        public override string AbAdd(int A)
                        {
                            return string.Empty;
                        }
                        public override string AbAdd(int A, int B)
                        {
                            return string.Empty;
                        }
                        public override void AbAdd(int A, int B, int C)
                        {
                            //base.AbAdd(A, B, C);
                        }
                        public override float Area()
                        {
                            return float.Parse("10.00");
                        }
                        public override float Circumference()
                        {
                            return float.Parse("10.00");
                        }
                    }

                }
                namespace AbstractsANDInterfaces
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public interface IEmployee
                    {
                        //just signature of the properties and methods.
                        //setting a rule or contract to be followed by implementations.
                        String ID
                        {
                            get;
                            set;
                        }

                        String FirstName
                        {
                            get;
                            set;
                        }

                        String LastName
                        {
                            get;
                            set;
                        }

                        // cannot have implementation
                        // cannot have modifiers public etc all are assumed public
                        // cannot have virtual
                        String Update();
                        String Add();
                        String Delete();
                        String Search();
                        String CalculateWage();
                    }
                    /// <summary>
                    /// Implementing the interface
                    /// </summary>
                    public class FulltimeInterface : IEmployee
                    {
                        //All the properties and fields are defined here!
                        protected String id;
                        protected String lname;
                        protected String fname;

                        public FulltimeInterface()
                        {
                            //
                            // TODO: Add constructor logic here
                            //
                        }

                        public String ID
                        {
                            get
                            {
                                return id;
                            }
                            set
                            {
                                id = value;
                            }
                        }

                        public String FirstName
                        {
                            get
                            {
                                return fname;
                            }
                            set
                            {
                                fname = value;
                            }
                        }

                        public String LastName
                        {
                            get
                            {
                                return lname;
                            }
                            set
                            {
                                lname = value;
                            }
                        }

                        //all the manipulations including Add,Delete, Search, Update, Calculate are done
                        //within the object as there are not implementation in the Interface entity.
                        public String Add()
                        {
                            return "Fulltime Employee " + fname + " added.";
                        }

                        public String Delete()
                        {
                            return "Fulltime Employee " + fname + " deleted.";
                        }

                        public String Search()
                        {
                            return "Fulltime Employee " + fname + " searched.";
                        }

                        public String Update()
                        {
                            return "Fulltime Employee " + fname + " updated.";
                        }

                        //if you change to Calculatewage(). Just small 'w' it will raise error as in interface
                        //it is CalculateWage() with capital 'W'.
                        public String CalculateWage()
                        {
                            return "Full time employee " + fname + " caluculated using Interface.";
                        }
                    }
                    /// <summary>
                    /// Summary description for Employee.
                    /// </summary>
                    public abstract class Employee
                    {
                        //we can have fields and properties in the Abstract class
                        protected String id;
                        protected String lname;
                        protected String fname;

                        //properties
                        public abstract String ID
                        {
                            get;
                            set;
                        }

                        public abstract String FirstName
                        {
                            get;
                            set;
                        }

                        public abstract String LastName
                        {
                            get;
                            set;
                        }
                        //completed methods
                        public String Update()
                        {
                            return "Employee " + id + " " + lname + " " + fname + " updated";
                        }
                        //completed methods
                        public String Add()
                        {
                            return "Employee " + id + " " + lname + " " + fname + " added";
                        }
                        //completed methods
                        public String Delete()
                        {
                            return "Employee " + id + " " + lname + " " + fname + " deleted";
                        }
                        //completed methods
                        public String Search()
                        {
                            return "Employee " + id + " " + lname + " " + fname + " found";
                        }

                        //abstract method that is different from Fulltime and Contractor
                        //therefore i keep it uncompleted and let each implementation 
                        //complete it the way they calculate the wage.
                        public abstract String CalculateWage();

                        public abstract String NewUpdate();

                        public abstract String NewAdd();

                        public abstract String NewDelete();

                        public abstract String NewSearch();

                        public abstract String NewCalculateWage();
                    }
                    /// <summary>
                    /// Summary description for Emp_Fulltime.
                    /// </summary>
                    //Inheriting from the Abstract class
                    public class FulltimeAbstract : Employee
                    {
                        //uses all the properties of the Abstract class
                        //therefore no properties or fields here!

                        public FulltimeAbstract()
                        {
                        }


                        public override String ID
                        {
                            get
                            {
                                return id;
                            }
                            set
                            {
                                id = value;
                            }
                        }

                        public override String FirstName
                        {
                            get
                            {
                                return fname;
                            }
                            set
                            {
                                fname = value;
                            }
                        }

                        public override String LastName
                        {
                            get
                            {
                                return lname;
                            }
                            set
                            {
                                lname = value;
                            }
                        }

                        //common methods that are implemented in the abstract class
                        public new String Add()
                        {
                            return base.Add();
                        }
                        //common methods that are implemented in the abstract class
                        public new String Delete()
                        {
                            return base.Delete();
                        }
                        //common methods that are implemented in the abstract class
                        public new String Search()
                        {
                            return base.Search();
                        }
                        //common methods that are implemented in the abstract class
                        public new String Update()
                        {
                            return base.Update();
                        }

                        //abstract method that is different from Fulltime and Contractor
                        //therefore I override it here.
                        public override String CalculateWage()
                        {
                            return "Full time employee " + base.fname + " is calculated using the Abstract class...";
                        }
                        public override string NewAdd()
                        {
                            return "Full time employee " + base.fname + " is calculated using the Abstract class...";
                        }
                        public override string NewDelete()
                        {
                            return "Full time employee " + base.fname + " is calculated using the Abstract class...";
                        }
                        public override string NewSearch()
                        {
                            return "Full time employee " + base.fname + " is calculated using the Abstract class...";
                        }
                        public override string NewCalculateWage()
                        {
                            return "Full time employee " + base.fname + " is calculated using the Abstract class...";
                        }
                        public override string NewUpdate()
                        {
                            return "Full time employee " + base.fname + " is calculated using the Abstract class...";
                        }
                    }
                }
            }
        }
    }
}

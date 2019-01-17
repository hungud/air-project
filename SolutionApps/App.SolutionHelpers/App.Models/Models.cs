using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utililty = App.Common.CommonUtility;
namespace App.Models
{

    #region RND Model
    public class HumanResources
    {
        public dynamic BusinessEntityID { get; set; }
        public dynamic NationalIDNumber { get; set; }
        public dynamic LoginID { get; set; }
        public dynamic OrganizationNode { get; set; }
        public dynamic OrganizationLevel { get; set; }
        public dynamic JobTitle { get; set; }
        public dynamic BirthDate { get; set; }
        public dynamic MaritalStatus { get; set; }
        public dynamic Gender { get; set; }
        public dynamic HireDate { get; set; }
        public dynamic SalariedFlag { get; set; }
        public dynamic VacationHours { get; set; }
        public dynamic SickLeaveHours { get; set; }
        public dynamic CurrentFlag { get; set; }
        public dynamic rowguid { get; set; }
        public dynamic ModifiedDate { get; set; }
    }

    public class Car
    {
        public Car() { }
        public dynamic Id { get; set; }
        public dynamic Make { get; set; }
        public dynamic Model { get; set; }
        public dynamic Year { get; set; }
        public dynamic Doors { get; set; }
        public dynamic Colour { get; set; }
        public dynamic Email { get; set; }
        public dynamic Price { get; set; }
        public dynamic Mileage { get; set; }
    }

    #region MultiModelsList
    public class MultiModelsList
    {
        public MultiModelsList()
        {

        }
        public List<Models1> MyModels1 { get; set; }
        public List<Models2> MyModels2 { get; set; }
        public List<Models3> MyModels3 { get; set; }
        public List<Models4> MyModels4 { get; set; }
        public List<Models5> MyModels5 { get; set; }
    }
    public class Models1
    {
        public Models1()
        {

        }
        public dynamic USERID { get; set; }
        public dynamic FIRSTNAME { get; set; }
        public dynamic LASTNAME { get; set; }
        public dynamic MOBILE { get; set; }
        public dynamic STATUS { get; set; }
        public dynamic Make { get; set; }
        public dynamic Model { get; set; }
        public dynamic Year { get; set; }
        public dynamic Doors { get; set; }
        public dynamic Colour { get; set; }
        public dynamic Email { get; set; }
        public dynamic Price { get; set; }
        public dynamic Mileage { get; set; }

    }
    public class Models2
    {
        public Models2()
        {

        }
        public dynamic USERID { get; set; }
        public dynamic FIRSTNAME { get; set; }
        public dynamic LASTNAME { get; set; }
        public dynamic MOBILE { get; set; }
        public dynamic STATUS { get; set; }
        public dynamic Make { get; set; }
        public dynamic Model { get; set; }
        public dynamic Year { get; set; }
        public dynamic Doors { get; set; }
        public dynamic Colour { get; set; }
        public dynamic Email { get; set; }
        public dynamic Price { get; set; }
        public dynamic Mileage { get; set; }

    }
    public class Models3
    {
        public Models3()
        {

        }
        public dynamic USERID { get; set; }
        public dynamic FIRSTNAME { get; set; }
        public dynamic LASTNAME { get; set; }
        public dynamic MOBILE { get; set; }
        public dynamic STATUS { get; set; }
        public dynamic Make { get; set; }
        public dynamic Model { get; set; }
        public dynamic Year { get; set; }
        public dynamic Doors { get; set; }
        public dynamic Colour { get; set; }
        public dynamic Email { get; set; }
        public dynamic Price { get; set; }
        public dynamic Mileage { get; set; }

    }
    public class Models4
    {
        public Models4()
        {

        }
        public dynamic USERID { get; set; }
        public dynamic FIRSTNAME { get; set; }
        public dynamic LASTNAME { get; set; }
        public dynamic MOBILE { get; set; }
        public dynamic STATUS { get; set; }
        public dynamic Make { get; set; }
        public dynamic Model { get; set; }
        public dynamic Year { get; set; }
        public dynamic Doors { get; set; }
        public dynamic Colour { get; set; }
        public dynamic Email { get; set; }
        public dynamic Price { get; set; }
        public dynamic Mileage { get; set; }

    }
    public class Models5
    {
        public Models5()
        {

        }
        public dynamic USERID { get; set; }
        public dynamic FIRSTNAME { get; set; }
        public dynamic LASTNAME { get; set; }
        public dynamic MOBILE { get; set; }
        public dynamic STATUS { get; set; }
        public dynamic Make { get; set; }
        public dynamic Model { get; set; }
        public dynamic Year { get; set; }
        public dynamic Doors { get; set; }
        public dynamic Colour { get; set; }
        public dynamic Email { get; set; }
        public dynamic Price { get; set; }
        public dynamic Mileage { get; set; }

    }

    #endregion MultiModelsList


    public class LoginInformation
    {
        public dynamic UserId { get; set; }
        public dynamic RoleId { get; set; }
        public dynamic Token { get; set; }
    }

    #endregion RND Model



}



using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestMethod]
        public void CreateProductOkTest()
        {//(active = 1)

        }
        //CREATE
        //Product no User 
        //No category (bien)
        //No Name
        //No Code
        //No Description
        //No manufacturer
        //No Price
        //Negative Price
        //Existing Name
        //Existing Code
        //Wrong Category
        //Create wrong User Role

        //MODIFY
        //OK
        //No category(bien)
        // Wrong Category
        //Product no User
        //No Name
        //No Code
        //No Description
        //No manufacturer
        //No Price
        //Negative Price
        //Existing Name
        //Existing Code
        //Wrong User Role
        //Not Existing Product
        //Existing Name
        //Existing Code

        //DELETE
        //OK
        //NO Product
        //Wrong User Role
        //Delete con ordenes con este producto (borro order products de ordenes haciendose), cambio active a 0

        //Change Category
        //Ok
        //Wrong Category
        //No category
        //No Product
        //Wrong User Role

        //ADD ATTRIBUTE
        //Ok
        //Already Added
        //NO Attribute
        //NO Produt
        //Wrong User Role
        //Value different from type


        //GET ALL 
        //Ok (no user)
        //Ok (user, with reviews)
        //Wrong User (Idem no user)

        //GET Filtered

        //GET - tambien devuelve attributtes
        //OK
        //Check attributes
        //No Product

        //GET MOST SOLD
        //ADD PICTURE
        //DELETE PICTURE





    }
}

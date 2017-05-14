using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Statuses_And_Roles;
using Repository;
using Services;
using Exceptions;

namespace ServicesTests
{
    [TestClass]
    public class FeatureServiceTests
    {
        GenericRepository<Feature> repo {
            get
            {
                if (repo == null)
                    return new GenericRepository<Feature>(true);
                return repo;
            }
        } 
        private FeatureService getService()
        {
            return new FeatureService(repo);
        }

        [TestMethod]
        public void AttributeCreateStringOkTest() {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Type = FeatureTypes.STRING;
            feature.Name = "Color";
            service.Add(feature);

            Feature savedFeature = repo.Get(feature.Id);
            Assert.IsNotNull(savedFeature);
        }

        [TestMethod]
        public void AttributeCreateIntOkTest()
        {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Type = FeatureTypes.INT;
            feature.Name = "Peso";
            service.Add(feature);

            Feature savedFeature = repo.Get(feature.Id);
            Assert.IsNotNull(savedFeature);

        }

        [TestMethod]
        public void AttributeCreateDateOkTest()
        {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Type = FeatureTypes.DATE;
            feature.Name = "Vencimiento";
            service.Add(feature);

            Feature savedFeature = repo.Get(feature.Id);
            Assert.IsNotNull(savedFeature);

        }

        [ExpectedException (typeof(FeatureWithoutTypeException))]
        [TestMethod]
        public void AttributeCreateNoTypeTest()
        {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Name = "Color";
            service.Add(feature);
        }

        [ExpectedException(typeof(FeatureWrongTypeException))]
        [TestMethod]
        public void AttributeCreateWrongTypeTest()
        {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Type = 10;
            feature.Name = "Color";
            service.Add(feature);
        }

        [ExpectedException(typeof(FeatureNoNameException))]
        [TestMethod]
        public void AttributeCreateNoNameTest()
        {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Type = 10;
            service.Add(feature);
        }

        [ExpectedException(typeof(FeatureExistingCombinationException))]
        [TestMethod]
        public void AttributeExistingTypeWithNameTest()
        {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Type = FeatureTypes.STRING;
            feature.Name = "Color";
            service.Add(feature);

            Feature feature2 = new Feature();
            feature2.Type = FeatureTypes.STRING;
            feature2.Name = "Color";
            service.Add(feature);
            service.Add(feature2);
        }

        public void AttributeGetAllTest() {
            FeatureService service = getService();
            Feature feature = new Feature();
            feature.Type = FeatureTypes.STRING;
            feature.Name = "Color";
            service.Add(feature);

            Feature feature2 = new Feature();
            feature2.Type = FeatureTypes.STRING;
            feature2.Name = "Talle";
            service.Add(feature);
            service.Add(feature2);

            List<Feature> allFeatures = service.GetAll();
            Assert.AreEqual(2, allFeatures.Count);
            Feature savedFeature = repo.Get(feature.Id);
            Assert.IsNotNull(savedFeature);
            Feature savedFeature2 = repo.Get(feature2.Id);
            Assert.IsNotNull(savedFeature2);
        }

    }
}

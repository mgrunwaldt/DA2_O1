using System;
using System.Collections.Generic;
using Entities;
using Repository;
using Exceptions;
namespace Services
{
    public class FeatureService
    {
        private GenericRepository<Feature> repo;

        public FeatureService(GenericRepository<Feature> repo)
        {
            this.repo = repo;
        }

        public void Add(Feature feature)
        {
            feature.Validate();
            checkForExistingFeature(feature);
            repo.Add(feature);
        }

        private void checkForExistingFeature(Feature f)
        {
            List < Feature > allFeatures = repo.GetAll();
            Feature existing = allFeatures.Find(feature => feature.Name == f.Name && feature.Type == f.Type);
            if (existing != null) {
                throw new FeatureExistingCombinationException("No se puede tener dos atributos del mismo tipo y mismo nombre");
            }
        }

        public List<Feature> GetAll()
        {
            return repo.GetAll();
        }
    }
}
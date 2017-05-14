using System;
using System.Collections.Generic;
using Entities;
using Repository;

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
            throw new NotImplementedException();
        }

        public List<Feature> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
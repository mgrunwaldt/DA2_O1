using Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using Entities.Statuses_And_Roles;
using System.Collections.Generic;

namespace Entities
{
    public class ProductFeature
    {
        public Guid FeatureId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        [Key]
        public Guid Id { get; set; }

        public ProductFeature()
        {
            this.Id = Guid.NewGuid();
        }

        public void Validate()
        {
            if (Value == null || Value.Trim() == "")
                throw new ProductFeatureNoValueException("No se puede agregar un atributo a un producto sin especificarle su valor");
        }

        public void CheckIfValueCorrespondsToType(Feature f)
        {
            if (f.Type == FeatureTypes.INT)
            {
                checkIfValueIsInt();
            }
            else if (f.Type == FeatureTypes.STRING)
            {
                checkIfValueIsString();
            }
            else if (f.Type == FeatureTypes.DATE)
            {
                checkIfValueIsDate();
            }
            else throw new FeatureWrongTypeException("El tipo solo puede ser 'texto', 'numero' o 'fecha'");
        }

        private void checkIfValueIsDate()
        {
            DateTime myDate;
            if (!DateTime.TryParse(Value, out myDate))
            {
                throw new ProductFeatureWrongValueException("El atributo ingresado debe ser una fecha con el siguiente formato: MM/DD/AAAA HH:MM:SS");
            }
        }

        private void checkIfValueIsString()
        {
            int intVal;
            if (Int32.TryParse(Value, out intVal))
                throw new ProductFeatureWrongValueException("El atributo ingresado debe ser un texto");
            DateTime myDate;
            if (DateTime.TryParse(Value, out myDate))
            {
                throw new ProductFeatureWrongValueException("El atributo ingresado debe ser un texto");
            }
        }

        private void checkIfValueIsInt()
        {
            int intVal;
            if (!Int32.TryParse(Value, out intVal))
                throw new ProductFeatureWrongValueException("El atributo ingresado debe ser un número entero");
        }
    }
}
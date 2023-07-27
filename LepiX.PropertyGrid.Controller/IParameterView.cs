//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using LepiX.PropertyGrid.Model;

namespace LepiX.PropertyGrid.Controller
{
    public interface IParametersView
    {
        void SetController(ParameterController controller);
        ParameterModel ParameterModel { get; set; }
    }
}

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

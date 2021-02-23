using CoursMCPDNETF.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Interfaces
{
    interface IDeformable
    {
        Figure Deformation(double coeffH, double coeffV);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    public interface IGenerator
    {
        int RandomPins(int max);
    }
}

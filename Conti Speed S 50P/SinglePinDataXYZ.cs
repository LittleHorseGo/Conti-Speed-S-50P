using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conti_Speed_S_50P
{
    public class AllPinDataXYZ
    {
        private const int PINNUM = 25;
        private double?[] _posXOrigData = new double?[PINNUM];
        private double?[] _posYOrigData = new double?[PINNUM];
        private double?[] _posZOrigData = new double?[PINNUM];

        public double?[] PosXOrigData { get => _posXOrigData; set => _posXOrigData = value; }
        public double?[] PosYOrigData { get => _posYOrigData; set => _posYOrigData = value; }
        public double?[] PosZOrigData { get => _posZOrigData; set => _posZOrigData = value; }
    }
}

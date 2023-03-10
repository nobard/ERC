using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERC.Models
{
    public static class CalculationAlgorithms
    {
        //рассчет за любую услугу
        public static float BaseCalculation(float volume, float tariff) => volume * tariff;

        //рассчет объема по показаниям
        public static float VBaseCalculation(float mCurr, float mPrev = 0f) => mCurr - mPrev;

        //рассчет объема без прибора учета
        public static float VIndicationCalculation(int residentsCount, float norm) => residentsCount * norm;

        //рассчет объема услуги ГВС
        public static float GvsTeCalculation(float vtn, float nte) => vtn * nte;
    }
}

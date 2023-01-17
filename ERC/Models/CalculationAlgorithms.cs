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
        public static float VIndicationCalculation(float residentsCount, float norm) => residentsCount * norm;

        //рассчет объема услуги ГВС
        public static float VGvsCalculation(float vtn, float nte) => vtn * nte;

        //рассчет услуги ЭЭ
        public static float EeCalculation(float volumeDay, float volumeNight, float tariffDay, float tariffNight) 
            => BaseCalculation(volumeDay, tariffDay) + BaseCalculation(volumeNight, tariffNight);
    }
}

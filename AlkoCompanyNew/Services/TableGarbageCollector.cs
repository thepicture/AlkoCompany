using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace AlkoCompanyNew
{
    public class TableGarbageCollector : IGarbageCollector
    {
        public void Collect()
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    foreach (Klient klient
                       in entities.Klient.ToList())
                    {
                        if (klient.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.Klient.Find(klient.K_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    foreach (ObjectAssessmentHouse house
                        in entities.ObjectAssessmentHouse.ToList())
                    {
                        if (house.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.ObjectAssessmentHouse.Find(house.OH_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    foreach (ObjectAssessmentGround ground
                        in entities.ObjectAssessmentGround.ToList())
                    {
                        if (ground.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.ObjectAssessmentGround.Find(ground.OG_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    foreach (ObjectAssessmentAll objectAll
                        in entities.ObjectAssessmentAll.ToList())
                    {
                        if (objectAll.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.ObjectAssessmentAll.Find(objectAll.OA_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось удалить " +
                    "объекты без заявок. " + ex);
            }
        }
    }
}

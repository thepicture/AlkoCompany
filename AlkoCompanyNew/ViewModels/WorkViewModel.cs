using AlkoCompanyNew.Commands;
using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Models.Enums;
using AlkoCompanyNew.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlkoCompanyNew.ViewModels
{
    public class WorkViewModel : ViewModelBase
    {
        private Zayavka zayavka;

        public WorkViewModel(Zayavka zayavka)
        {
            Zayavka = zayavka;
            AdditionalBuildingTypes = new ObservableCollection<string>(
                new string[]
                {
                    "Отсутствуют",
                    "Присутствуют"
                });
            TypeBlagoustroistva = new ObservableCollection<string>(
                new string[]
                {
                    "Не благоустроен",
                    "Частично благоустроен",
                    "Благоустроен"
                });
            TypeMebel = new ObservableCollection<string>(
                new string[]
                {
                    "Отсутствует",
                    "Присутствует"
                });
            TypeMaterialSten = new ObservableCollection<string>(
                new string[]
                {
                    "Блок",
                    "Кирпич",
                    "Бревно",
                    "Каркас",
                    "Монолит"
                });
            TypeOtoplenia = new ObservableCollection<string>(
                new string[]
                {
                    "Автономное, электрическое",
                    "Автономное, газовое",
                    "Автономное, печное",
                    "Центральное"
                });
            TypeVoda = new ObservableCollection<string>(
                new string[]
                {
                    "Автономное",
                    "Центральное"
                });
            TypeKanalizacia = new ObservableCollection<string>(
                new string[]
                {
                    "Автономная",
                    "Центральная"
                });
            TypeFizSostoyanie = new ObservableCollection<string>(
                new string[]
                {
                    "Хорошее",
                    "Среднее",
                    "Плохое"
                });
            TypeVnutrOtdelki = new ObservableCollection<string>(
                new string[]
                {
                    "Высококачественная внутренняя отделка",
                    "Улучшенная (повышенная) внутренняя отделка",
                    "Простая внутренняя отделка",
                    "Требуется космитический ремонт",
                    "Без внутренней отделки"
                });
            TypeNarugOtdelki = new ObservableCollection<string>(
                new string[]
                {
                    "Высококачественная отделка",
                    "Простая отделка",
                    "Без отделки"
                });
            TypesOfGround = new ObservableCollection<string>(
              new string[]
              {
                    "ЗНП",
                    "Тип земли 2",
                    "Тип земли 3"
              });
            AllowedUsages = new ObservableCollection<string>(
             new string[]
             {
                    "ИЖС",
                    "Тип разрешённого использования 2",
                    "Тип разрешённого использования 3"
             });
            VozmognostPodkluchenia = new ObservableCollection<string>(
             new string[]
             {
                    "Да",
                    "Нет"
             });

            if (zayavka.ObjectAssessmentHouse == null)
            {
                try
                {
                    zayavka.ObjectAssessmentHouse =
                        new ObjectAssessmentHouse();
                    for (int i = 0; i < 3; i++)
                    {
                        zayavka.ObjectAssessmentHouse.AnalogiHouse.Add(
                            new AnalogiHouse());
                    }
                    AppData.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            if (zayavka.ObjectAssessmentGround == null)
            {
                try
                {
                    zayavka.ObjectAssessmentGround =
                        new ObjectAssessmentGround();
                    for (int i = 0; i < 3; i++)
                    {
                        zayavka.ObjectAssessmentGround.AnalogiGround.Add(
                            new AnalogiGround());
                    }
                    AppData.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            if (zayavka.ObjectAssessmentAll == null)
            {
                try
                {
                    zayavka.ObjectAssessmentAll =
                        new ObjectAssessmentAll();
                    AppData.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            AssessmentObject = zayavka.ObjectAssessmentHouse;
            AssessmentGround = zayavka.ObjectAssessmentGround;
            AssessmentAll = zayavka.ObjectAssessmentAll;
            AnalogueHouses = new ObservableCollection<AnalogiHouse>
                (AssessmentObject.AnalogiHouse);
            foreach (AnalogiHouse analogueHouse in AnalogueHouses)
            {
                analogueHouse.NumberOfAnalogue
                    = AnalogueHouses.IndexOf(analogueHouse) + 1;
                EventInfo analogueHouseEventInfo = analogueHouse
                    .GetType()
                    .GetEvent(
                        nameof(PropertyChanged));
                analogueHouseEventInfo.AddEventHandler(analogueHouse,
                    Delegate.CreateDelegate(
                        analogueHouseEventInfo.EventHandlerType,
                        this,
                        GetType()
                            .GetMethod(
                                nameof(PerformChangeContext))));
            }
            AnalogueGrounds = new ObservableCollection<AnalogiGround>
                (AssessmentGround.AnalogiGround);
            foreach (AnalogiGround analogueGround in AnalogueGrounds)
            {
                analogueGround.NumberOfAnalogue
                    = AnalogueGrounds.IndexOf(analogueGround) + 1;
                EventInfo analogueGroundEventInfo = analogueGround
                    .GetType()
                    .GetEvent(
                        nameof(PropertyChanged));
                analogueGroundEventInfo.AddEventHandler(analogueGround,
                    Delegate.CreateDelegate(
                        analogueGroundEventInfo.EventHandlerType,
                        this,
                        GetType()
                            .GetMethod(
                                nameof(PerformChangeContext))));
            }
            EventInfo assessmentObjectEventInfo = AssessmentObject
                    .GetType()
                    .GetEvent(
                        nameof(PropertyChanged));
            assessmentObjectEventInfo.AddEventHandler(AssessmentObject,
                Delegate.CreateDelegate(
                    assessmentObjectEventInfo.EventHandlerType,
                    this,
                    GetType()
                        .GetMethod(
                            nameof(PerformChangeContext))));
            EventInfo assessmentGroundEventInfo = AssessmentGround
                   .GetType()
                   .GetEvent(
                       nameof(PropertyChanged));
            assessmentGroundEventInfo.AddEventHandler(AssessmentGround,
                Delegate.CreateDelegate(
                    assessmentGroundEventInfo.EventHandlerType,
                    this,
                    GetType()
                        .GetMethod(
                            nameof(PerformChangeContext))));
            EventInfo assessmentAllEventInfo = AssessmentAll
                 .GetType()
                 .GetEvent(
                     nameof(PropertyChanged));
            assessmentAllEventInfo.AddEventHandler(AssessmentAll,
                Delegate.CreateDelegate(
                    assessmentAllEventInfo.EventHandlerType,
                    this,
                    GetType()
                        .GetMethod(
                            nameof(PerformChangeContext))));
            EventInfo clientEventInfo = Zayavka.Klient
                   .GetType()
                   .GetEvent(
                       nameof(PropertyChanged));
            clientEventInfo.AddEventHandler(Zayavka.Klient,
                Delegate.CreateDelegate(
                    clientEventInfo.EventHandlerType,
                    this,
                    GetType()
                        .GetMethod(
                            nameof(PerformChangeContext))));
            PerformChangeContext(this,
                                 new PropertyChangedEventArgs(""));
        }

        /// <summary>
        /// Выполняется, если изменяется любое свойство 
        /// объекта оценки или если изменяются аналоги.
        /// Вычисление происходит в регионе <see langword="calculation"/>.
        /// Все аналоги и объект оценки, относящийся к этим аналогам, 
        /// будут сохранены в базу данных после любого изменения свойств 
        /// в реальном времени без необходимости нажатия кнопки "Сохранить".
        /// Доступ к аналогам объекта оценки происходит по индексу.
        /// 
        /// Вычисляемые свойства помечаются с префиксом "C": OH_C_Correction.
        /// 
        /// Если необходим мнгновенный пересчёт ячеек, необходимо поставить режим 
        /// <see langword="UpdateSourceTrigger"/> в режим <see langword="PropertyChanged"/>:
        /// &lt;TextBox Text="{Binding Address, UpdateSourceTrigger=PropertyChanged}"/>
        /// </summary>
        /// <example>
        /// Автоматическое изменение корректировки объекта оценки 
        /// на основе вычисления среднего значения между корректировкой аналогов:
        /// 
        ///  #region calculation
        ///    AssessmentObject.OH_C_Correction = AssessmentObject
        ///         .AnalogiHouse
        ///         .Average(ah => ah.AH_Correction);
        ///  #endregion
        /// </example>
        /// <example>
        /// Получение первого, второго и третьего аналогов:
        /// 
        ///  #region calculation
        ///      AssessmentObject.OH_C_Correction
        ///      = AnalogueHouses[0].AH_Correction
        ///      + AnalogueHouses[1].AH_Correction 
        ///      + AnalogueHouses[2].AH_Correction;
        ///  #endregion
        /// </example>
        public void PerformChangeContext(object sender, PropertyChangedEventArgs e)
        {
            if (IsUpdating) return;
            IsUpdating = true;
            #region houseCalculation
            // GetNonZero вернёт 1, если корректировка была равна 0 или была равна null, чтоб результат не занулялся, иначе саму корректировку
            // то есть 0 станет 1,
            // null станет 1
            // 0.8 станет 0.8

            // цена кв.м
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiVse / AnalogueHouses[0].AH_Ploshad;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiVse / AnalogueHouses[1].AH_Ploshad;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiVse / AnalogueHouses[2].AH_Ploshad;
            //
            // корректировка на торг (в одну строчку)
            // может использоваться, если формула остаётся неизменной и изменяются только индексы от 0 до 2
            // i меняется от 0 до 2
            //
            //Execute(i => AnalogueHouses[i].AH_CenaProdazhiKvm = AnalogueHouses[i].AH_CenaProdazhiKvm * ПолучитьКорректировку(AnalogueHouses[i].AH_KorTorg));
            //
            // или ещё короче с использованием оператора *=
            // x *= y это то же самое что x = x * y, можно использовать, если результат надо на что-то домножить
            //Execute(i => AnalogueHouses[i].AH_CenaProdazhiKvm *= ПолучитьКорректировку(AnalogueHouses[i].AH_KorTorg));

            // корректировка на торг (как было)
            AnalogueHouses[0].AH_KorTorgCenaAfter = AnalogueHouses[0].AH_CenaProdazhiKvm * ПолучитьКорректировку(AnalogueHouses[0].AH_KorTorg);
            AnalogueHouses[1].AH_KorTorgCenaAfter = AnalogueHouses[1].AH_CenaProdazhiKvm * ПолучитьКорректировку(AnalogueHouses[1].AH_KorTorg);
            AnalogueHouses[2].AH_KorTorgCenaAfter = AnalogueHouses[2].AH_CenaProdazhiKvm * ПолучитьКорректировку(AnalogueHouses[2].AH_KorTorg);
            // корректировка на дату продажи
            AnalogueHouses[0].AH_KorNaDatuProdazhiCenaAfter = AnalogueHouses[0].AH_KorTorgCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaDatuProdazhi);
            AnalogueHouses[1].AH_KorNaDatuProdazhiCenaAfter = AnalogueHouses[1].AH_KorTorgCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaDatuProdazhi);
            AnalogueHouses[2].AH_KorNaDatuProdazhiCenaAfter = AnalogueHouses[2].AH_KorTorgCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaDatuProdazhi);
            // корректировка на месторасположение
            AnalogueHouses[0].AH_KorNaMestoCenaAfter = AnalogueHouses[0].AH_KorNaDatuProdazhiCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaMesto);
            AnalogueHouses[1].AH_KorNaMestoCenaAfter = AnalogueHouses[1].AH_KorNaDatuProdazhiCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaMesto);
            AnalogueHouses[2].AH_KorNaMestoCenaAfter = AnalogueHouses[2].AH_KorNaDatuProdazhiCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaMesto);
            // корректировка на дополнительные постройки 
            AnalogueHouses[0].AH_KorNaDopPostroykiCenaAfter = AnalogueHouses[0].AH_KorNaMestoCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaDopPostroyki);
            AnalogueHouses[1].AH_KorNaDopPostroykiCenaAfter = AnalogueHouses[1].AH_KorNaMestoCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaDopPostroyki);
            AnalogueHouses[2].AH_KorNaDopPostroykiCenaAfter = AnalogueHouses[2].AH_KorNaMestoCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaDopPostroyki);
            // корректировка на благоустройство участка
            AnalogueHouses[0].AH_KorNaBlagoustroistvoCenaAfter = AnalogueHouses[0].AH_KorNaDopPostroykiCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaBlagoustroistvo);
            AnalogueHouses[1].AH_KorNaBlagoustroistvoCenaAfter = AnalogueHouses[1].AH_KorNaDopPostroykiCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaBlagoustroistvo);
            AnalogueHouses[2].AH_KorNaBlagoustroistvoCenaAfter = AnalogueHouses[2].AH_KorNaDopPostroykiCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaBlagoustroistvo);
            // корректировка на мебель
            AnalogueHouses[0].AH_KorNaMebelCenaAfter = AnalogueHouses[0].AH_KorNaBlagoustroistvoCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaMebel);
            AnalogueHouses[1].AH_KorNaMebelCenaAfter = AnalogueHouses[1].AH_KorNaBlagoustroistvoCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaMebel);
            AnalogueHouses[2].AH_KorNaMebelCenaAfter = AnalogueHouses[2].AH_KorNaBlagoustroistvoCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaMebel);
            // полная цена для вычета стоимости земли
            AnalogueHouses[0].AH_OrdinaryCenaAfter = AnalogueHouses[0].AH_KorNaMebelCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_Ploshad);
            AnalogueHouses[1].AH_OrdinaryCenaAfter = AnalogueHouses[1].AH_KorNaMebelCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_Ploshad);
            AnalogueHouses[2].AH_OrdinaryCenaAfter = AnalogueHouses[2].AH_KorNaMebelCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_Ploshad);
            // корректировка на масштаб
            const float nonSenseCoefficient = -0.127f;
            ////если надо возвести в степень числа
            //AnalogueGrounds[0].AG_NaMashtabCenaAfter = ВозвестиВ(степеньЧисла: 2, аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[0].AG_Ploshad));
            ////если надо возвести в степень переменной
            //AnalogueGrounds[1].AG_NaMashtabCenaAfter = ВозвестиВСтепень(переменной: power, аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[1].AG_Ploshad));

            // здесь будет 1
            AnalogueHouses[0].AH_KorNaMashtabZemli = ВозвестиВСтепень(переменной: nonSenseCoefficient, аргумент: AnalogueHouses[0].AH_PloshadZemli / ПолучитьКорректировку(AssessmentGround.OG_Ploshad));
            AnalogueHouses[1].AH_KorNaMashtabZemli = ВозвестиВСтепень(переменной: nonSenseCoefficient, аргумент: AnalogueHouses[1].AH_PloshadZemli / ПолучитьКорректировку(AssessmentGround.OG_Ploshad));
            AnalogueHouses[2].AH_KorNaMashtabZemli = ВозвестиВСтепень(переменной: nonSenseCoefficient, аргумент: AnalogueHouses[2].AH_PloshadZemli / ПолучитьКорректировку(AssessmentGround.OG_Ploshad));
            // корректировка на масштаб цена после
            AnalogueHouses[0].AH_KorNaMashtabCenaAfter = AnalogueHouses[0].AH_KorNaMashtabZemli * ПолучитьКорректировку(AssessmentGround.OG_CenaKvm);
            AnalogueHouses[1].AH_KorNaMashtabCenaAfter = AnalogueHouses[1].AH_KorNaMashtabZemli * ПолучитьКорректировку(AssessmentGround.OG_CenaKvm);
            AnalogueHouses[2].AH_KorNaMashtabCenaAfter = AnalogueHouses[2].AH_KorNaMashtabZemli * ПолучитьКорректировку(AssessmentGround.OG_CenaKvm);
            // стоимость земли аналога
            AnalogueHouses[0].AH_StoimostZemliAnaloga = AnalogueHouses[0].AH_KorNaMashtabCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_PloshadZemli);
            AnalogueHouses[1].AH_StoimostZemliAnaloga = AnalogueHouses[1].AH_KorNaMashtabCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_PloshadZemli);
            AnalogueHouses[2].AH_StoimostZemliAnaloga = AnalogueHouses[2].AH_KorNaMashtabCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_PloshadZemli);
            // стоимость объекта капитального строительства
            AnalogueHouses[0].AH_StoimostObjectaCapStroy = AnalogueHouses[0].AH_OrdinaryCenaAfter - ПолучитьКорректировку(AnalogueHouses[0].AH_StoimostZemliAnaloga);
            AnalogueHouses[1].AH_StoimostObjectaCapStroy = AnalogueHouses[1].AH_OrdinaryCenaAfter - ПолучитьКорректировку(AnalogueHouses[1].AH_StoimostZemliAnaloga);
            AnalogueHouses[2].AH_StoimostObjectaCapStroy = AnalogueHouses[2].AH_OrdinaryCenaAfter - ПолучитьКорректировку(AnalogueHouses[2].AH_StoimostZemliAnaloga);
            // стоимость объекта капитального строительства кв.м
            AnalogueHouses[0].AH_StoimostObjectaCapStroyKvm = AnalogueHouses[0].AH_StoimostObjectaCapStroy / ПолучитьКорректировку(AnalogueHouses[0].AH_Ploshad);
            AnalogueHouses[1].AH_StoimostObjectaCapStroyKvm = AnalogueHouses[1].AH_StoimostObjectaCapStroy / ПолучитьКорректировку(AnalogueHouses[1].AH_Ploshad);
            AnalogueHouses[2].AH_StoimostObjectaCapStroyKvm = AnalogueHouses[2].AH_StoimostObjectaCapStroy / ПолучитьКорректировку(AnalogueHouses[2].AH_Ploshad);
            // корректировка на этажность
            AnalogueHouses[0].AH_KorNaEtageCenaAfter = AnalogueHouses[0].AH_StoimostObjectaCapStroyKvm * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaEtage);
            AnalogueHouses[1].AH_KorNaEtageCenaAfter = AnalogueHouses[1].AH_StoimostObjectaCapStroyKvm * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaEtage);
            AnalogueHouses[2].AH_KorNaEtageCenaAfter = AnalogueHouses[2].AH_StoimostObjectaCapStroyKvm * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaEtage);
            // корректировка на материал стен
            AnalogueHouses[0].AH_KorNaMaterialStenCenaAfter = AnalogueHouses[0].AH_KorNaEtageCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaMaterialSten);
            AnalogueHouses[1].AH_KorNaMaterialStenCenaAfter = AnalogueHouses[1].AH_KorNaEtageCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaMaterialSten);
            AnalogueHouses[2].AH_KorNaMaterialStenCenaAfter = AnalogueHouses[2].AH_KorNaEtageCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaMaterialSten);
            // корректировка на тип отопления
            AnalogueHouses[0].AH_KorNaOtoplenieCenaAfter = AnalogueHouses[0].AH_KorNaMaterialStenCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaOtoplenie);
            AnalogueHouses[1].AH_KorNaOtoplenieCenaAfter = AnalogueHouses[1].AH_KorNaMaterialStenCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaOtoplenie);
            AnalogueHouses[2].AH_KorNaOtoplenieCenaAfter = AnalogueHouses[2].AH_KorNaMaterialStenCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaOtoplenie);
            // корректировка на тип воды
            AnalogueHouses[0].AH_KorNaVodaCenaAfter = AnalogueHouses[0].AH_KorNaOtoplenieCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaVoda);
            AnalogueHouses[1].AH_KorNaVodaCenaAfter = AnalogueHouses[1].AH_KorNaOtoplenieCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaVoda);
            AnalogueHouses[2].AH_KorNaVodaCenaAfter = AnalogueHouses[2].AH_KorNaOtoplenieCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaVoda);
            // корректировка на тип канализации
            AnalogueHouses[0].AH_KorNaKanalizaciaCenaAfter = AnalogueHouses[0].AH_KorNaVodaCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaKanalizacia);
            AnalogueHouses[1].AH_KorNaKanalizaciaCenaAfter = AnalogueHouses[1].AH_KorNaVodaCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaKanalizacia);
            AnalogueHouses[2].AH_KorNaKanalizaciaCenaAfter = AnalogueHouses[2].AH_KorNaVodaCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaKanalizacia);
            // корректировка на физическое состояние
            AnalogueHouses[0].AH_KorNaFizSostoyanieCenaAfter = AnalogueHouses[0].AH_KorNaKanalizaciaCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaFizSostoyanie);
            AnalogueHouses[1].AH_KorNaFizSostoyanieCenaAfter = AnalogueHouses[1].AH_KorNaKanalizaciaCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaFizSostoyanie);
            AnalogueHouses[2].AH_KorNaFizSostoyanieCenaAfter = AnalogueHouses[2].AH_KorNaKanalizaciaCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaFizSostoyanie);
            // корректировка на площадь
            const float nonPloshadCoefficient = -0.16f;
            AnalogueHouses[0].AH_KorNaPloshad = ВозвестиВСтепень(переменной: nonPloshadCoefficient, аргумент: AssessmentObject.OH_Ploshad / ПолучитьКорректировку(AnalogueHouses[0].AH_Ploshad));
            AnalogueHouses[1].AH_KorNaPloshad = ВозвестиВСтепень(переменной: nonPloshadCoefficient, аргумент: AssessmentObject.OH_Ploshad / ПолучитьКорректировку(AnalogueHouses[1].AH_Ploshad));
            AnalogueHouses[2].AH_KorNaPloshad = ВозвестиВСтепень(переменной: nonPloshadCoefficient, аргумент: AssessmentObject.OH_Ploshad / ПолучитьКорректировку(AnalogueHouses[2].AH_Ploshad));
            // корректировка на площадь цена после
            AnalogueHouses[0].AH_KorNaPloshadCenaAfter = AnalogueHouses[0].AH_KorNaPloshad * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaFizSostoyanieCenaAfter);
            AnalogueHouses[1].AH_KorNaPloshadCenaAfter = AnalogueHouses[1].AH_KorNaPloshad * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaFizSostoyanieCenaAfter);
            AnalogueHouses[2].AH_KorNaPloshadCenaAfter = AnalogueHouses[2].AH_KorNaPloshad * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaFizSostoyanieCenaAfter);
            // корректировка на внутреннюю отделку
            AnalogueHouses[0].AH_KorNaVnutrenyuOtdelkyCenaAfter = AnalogueHouses[0].AH_KorNaPloshadCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaVnutrenyuOtdelky);
            AnalogueHouses[1].AH_KorNaVnutrenyuOtdelkyCenaAfter = AnalogueHouses[1].AH_KorNaPloshadCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaVnutrenyuOtdelky);
            AnalogueHouses[2].AH_KorNaVnutrenyuOtdelkyCenaAfter = AnalogueHouses[2].AH_KorNaPloshadCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaVnutrenyuOtdelky);
            // корректировка на наружную отделку
            AnalogueHouses[0].AH_KorNaNarugnyuOtdelkyCenaAfter = AnalogueHouses[0].AH_KorNaVnutrenyuOtdelkyCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaNarugnyuOtdelky);
            AnalogueHouses[1].AH_KorNaNarugnyuOtdelkyCenaAfter = AnalogueHouses[1].AH_KorNaVnutrenyuOtdelkyCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaNarugnyuOtdelky);
            AnalogueHouses[2].AH_KorNaNarugnyuOtdelkyCenaAfter = AnalogueHouses[2].AH_KorNaVnutrenyuOtdelkyCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaNarugnyuOtdelky);
            // скорр. цена улучшений
            AnalogueHouses[0].AH_ScorrCenaUlucheniy = AnalogueHouses[0].AH_KorNaNarugnyuOtdelkyCenaAfter * ПолучитьКорректировку(AnalogueHouses[0].AH_Ploshad);
            AnalogueHouses[1].AH_ScorrCenaUlucheniy = AnalogueHouses[1].AH_KorNaNarugnyuOtdelkyCenaAfter * ПолучитьКорректировку(AnalogueHouses[1].AH_Ploshad);
            AnalogueHouses[2].AH_ScorrCenaUlucheniy = AnalogueHouses[2].AH_KorNaNarugnyuOtdelkyCenaAfter * ПолучитьКорректировку(AnalogueHouses[2].AH_Ploshad);
            // скорр. цена объектов
            AnalogueHouses[0].AH_ScorrCenaObjects = AnalogueHouses[0].AH_ScorrCenaUlucheniy + ПолучитьКорректировку(AnalogueHouses[0].AH_StoimostZemliAnaloga);
            AnalogueHouses[1].AH_ScorrCenaObjects = AnalogueHouses[1].AH_ScorrCenaUlucheniy + ПолучитьКорректировку(AnalogueHouses[1].AH_StoimostZemliAnaloga);
            AnalogueHouses[2].AH_ScorrCenaObjects = AnalogueHouses[2].AH_ScorrCenaUlucheniy + ПолучитьКорректировку(AnalogueHouses[2].AH_StoimostZemliAnaloga);
            // скорр. цена объектов за 1 квм
            AnalogueHouses[0].AH_ScorrCenaObjectsKvm = AnalogueHouses[0].AH_ScorrCenaObjects / ПолучитьКорректировку(AnalogueHouses[0].AH_Ploshad);
            AnalogueHouses[1].AH_ScorrCenaObjectsKvm = AnalogueHouses[1].AH_ScorrCenaObjects / ПолучитьКорректировку(AnalogueHouses[1].AH_Ploshad);
            AnalogueHouses[2].AH_ScorrCenaObjectsKvm = AnalogueHouses[2].AH_ScorrCenaObjects / ПолучитьКорректировку(AnalogueHouses[2].AH_Ploshad);
            // процент весового коэффициента
            Execute(i =>
            {
                float? absoluteTorgCenaDifference = ПолучитьАбсолютноеЗначение(AnalogueHouses[i].AH_ScorrCenaObjectsKvm - AnalogueHouses[i].AH_KorTorgCenaAfter);
                float? dividedTorgCena = absoluteTorgCenaDifference / AnalogueHouses[i].AH_KorTorgCenaAfter;
                AnalogueHouses[i].AH_VesovoyKoef = 1 - dividedTorgCena;
            });
            // общий процент
            AssessmentObject.OH_GeneralProcent = AnalogueHouses[0].AH_VesovoyKoef + AnalogueHouses[1].AH_VesovoyKoef + AnalogueHouses[2].AH_VesovoyKoef;
            // весовой коэффициент
            AnalogueHouses[0].AH_VesovoyKoef2 = AnalogueHouses[0].AH_VesovoyKoef / ПолучитьКорректировку(AssessmentObject.OH_GeneralProcent);
            AnalogueHouses[1].AH_VesovoyKoef2 = AnalogueHouses[1].AH_VesovoyKoef / ПолучитьКорректировку(AssessmentObject.OH_GeneralProcent);
            AnalogueHouses[2].AH_VesovoyKoef2 = AnalogueHouses[2].AH_VesovoyKoef / ПолучитьКорректировку(AssessmentObject.OH_GeneralProcent);
            // цена после весового коэффициента
            AnalogueHouses[0].AH_VesovoyKoef2CenaAfter = AnalogueHouses[0].AH_VesovoyKoef2 * ПолучитьКорректировку(AnalogueHouses[0].AH_KorNaNarugnyuOtdelkyCenaAfter);
            AnalogueHouses[1].AH_VesovoyKoef2CenaAfter = AnalogueHouses[1].AH_VesovoyKoef2 * ПолучитьКорректировку(AnalogueHouses[1].AH_KorNaNarugnyuOtdelkyCenaAfter);
            AnalogueHouses[2].AH_VesovoyKoef2CenaAfter = AnalogueHouses[2].AH_VesovoyKoef2 * ПолучитьКорректировку(AnalogueHouses[2].AH_KorNaNarugnyuOtdelkyCenaAfter);
            // цена за квадратный метр дома 
            AssessmentObject.OH_CenaKvm = AnalogueHouses[0].AH_VesovoyKoef2CenaAfter + AnalogueHouses[1].AH_VesovoyKoef2CenaAfter + AnalogueHouses[2].AH_VesovoyKoef2CenaAfter;
            // цена дома 
            AssessmentObject.OH_CenaVse = AssessmentObject.OH_CenaKvm * AssessmentObject.OH_Ploshad;
            

            // новая цена (атрибута в бд нет)
            //AnalogueHouses[0].NewKwm = AnalogueHouses[0].AH_CenaProdazhiKvm;
            //AnalogueHouses[1].NewKwm = AnalogueHouses[1].AH_CenaProdazhiKvm;
            //AnalogueHouses[2].NewKwm = AnalogueHouses[2].AH_CenaProdazhiKvm;
            #endregion
            #region groundCalculation
            // цена кв.м
            AnalogueGrounds[0].AG_CenaProdazhiKvm = AnalogueGrounds[0].AG_CenaProdazhiVse / ПолучитьКорректировку(AnalogueGrounds[0].AG_Ploshad);
            AnalogueGrounds[1].AG_CenaProdazhiKvm = AnalogueGrounds[1].AG_CenaProdazhiVse / ПолучитьКорректировку(AnalogueGrounds[1].AG_Ploshad);
            AnalogueGrounds[2].AG_CenaProdazhiKvm = AnalogueGrounds[2].AG_CenaProdazhiVse / ПолучитьКорректировку(AnalogueGrounds[2].AG_Ploshad);
            // Поправка на торг
            AnalogueGrounds[0].AG_TorgCenaAfter = AnalogueGrounds[0].AG_CenaProdazhiKvm * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorTorg);
            AnalogueGrounds[1].AG_TorgCenaAfter = AnalogueGrounds[1].AG_CenaProdazhiKvm * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorTorg);
            AnalogueGrounds[2].AG_TorgCenaAfter = AnalogueGrounds[2].AG_CenaProdazhiKvm * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorTorg);
            // корректировка на права
            AnalogueGrounds[0].AG_NaPravaCenaAfter = AnalogueGrounds[0].AG_TorgCenaAfter * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorNaPrava);
            AnalogueGrounds[1].AG_NaPravaCenaAfter = AnalogueGrounds[1].AG_TorgCenaAfter * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorNaPrava);
            AnalogueGrounds[2].AG_NaPravaCenaAfter = AnalogueGrounds[2].AG_TorgCenaAfter * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorNaPrava);
            // корректировка на дату продажи
            AnalogueGrounds[0].AG_NaDatuProdazhiCenaAfter = AnalogueGrounds[0].AG_NaPravaCenaAfter * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorNaDatuProdazhi);
            AnalogueGrounds[1].AG_NaDatuProdazhiCenaAfter = AnalogueGrounds[1].AG_NaPravaCenaAfter * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorNaDatuProdazhi);
            AnalogueGrounds[2].AG_NaDatuProdazhiCenaAfter = AnalogueGrounds[2].AG_NaPravaCenaAfter * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorNaDatuProdazhi);
            // корректировка на место
            AnalogueGrounds[0].AG_NaMestoCenaAfter = AnalogueGrounds[0].AG_NaDatuProdazhiCenaAfter * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorNaMesto);
            AnalogueGrounds[1].AG_NaMestoCenaAfter = AnalogueGrounds[1].AG_NaDatuProdazhiCenaAfter * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorNaMesto);
            AnalogueGrounds[2].AG_NaMestoCenaAfter = AnalogueGrounds[2].AG_NaDatuProdazhiCenaAfter * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorNaMesto);
            // корректировка на газ
            AnalogueGrounds[0].AG_NaGasCenaAfter = AnalogueGrounds[0].AG_NaMestoCenaAfter * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorNaGas);
            AnalogueGrounds[1].AG_NaGasCenaAfter = AnalogueGrounds[1].AG_NaMestoCenaAfter * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorNaGas);
            AnalogueGrounds[2].AG_NaGasCenaAfter = AnalogueGrounds[2].AG_NaMestoCenaAfter * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorNaGas);
            // корректировка на воду
            AnalogueGrounds[0].AG_NaVodaCenaAfter = AnalogueGrounds[0].AG_NaGasCenaAfter * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorNaVoda);
            AnalogueGrounds[1].AG_NaVodaCenaAfter = AnalogueGrounds[1].AG_NaGasCenaAfter * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorNaVoda);
            AnalogueGrounds[2].AG_NaVodaCenaAfter = AnalogueGrounds[2].AG_NaGasCenaAfter * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorNaVoda);
            // корректировка на электричество
            AnalogueGrounds[0].AG_NaElectricCenaAfter = AnalogueGrounds[0].AG_NaVodaCenaAfter * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorNaElecric);
            AnalogueGrounds[1].AG_NaElectricCenaAfter = AnalogueGrounds[1].AG_NaVodaCenaAfter * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorNaElecric);
            AnalogueGrounds[2].AG_NaElectricCenaAfter = AnalogueGrounds[2].AG_NaVodaCenaAfter * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorNaElecric);

            // корректировка на масштаб
            
            ////если надо возвести в степень числа
            //AnalogueGrounds[0].AG_NaMashtabCenaAfter = ВозвестиВ(степеньЧисла: 2, аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[0].AG_Ploshad));
            ////если надо возвести в степень переменной
            //AnalogueGrounds[1].AG_NaMashtabCenaAfter = ВозвестиВСтепень(переменной: power, аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[1].AG_Ploshad));

            // здесь будет 1
            AnalogueGrounds[0].AG_KorNaMashtab = ВозвестиВСтепень(переменной: nonSenseCoefficient, аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[0].AG_Ploshad));
            AnalogueGrounds[1].AG_KorNaMashtab = ВозвестиВСтепень(переменной: nonSenseCoefficient, аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[1].AG_Ploshad));
            AnalogueGrounds[2].AG_KorNaMashtab = ВозвестиВСтепень(переменной: nonSenseCoefficient, аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[2].AG_Ploshad));

            // корректировка на масштаб цена после
            AnalogueGrounds[0].AG_NaMashtabCenaAfter2 = AnalogueGrounds[0].AG_NaElectricCenaAfter * ПолучитьКорректировку(AnalogueGrounds[0].AG_KorNaMashtab);
            AnalogueGrounds[1].AG_NaMashtabCenaAfter2 = AnalogueGrounds[1].AG_NaElectricCenaAfter * ПолучитьКорректировку(AnalogueGrounds[1].AG_KorNaMashtab);
            AnalogueGrounds[2].AG_NaMashtabCenaAfter2 = AnalogueGrounds[2].AG_NaElectricCenaAfter * ПолучитьКорректировку(AnalogueGrounds[2].AG_KorNaMashtab);
            // весовой коэффициент в процентах
            Execute(i =>
            {
                float? torgCenaAfterAfter = ПолучитьКорректировку(AnalogueGrounds[i].AG_KorNaMashtab) * AnalogueGrounds[i].AG_TorgCenaAfter;
                float? absoluteTorgCenaDifference = ПолучитьАбсолютноеЗначение(AnalogueGrounds[i].AG_TorgCenaAfter - torgCenaAfterAfter);
                AnalogueGrounds[i].AG_VesovoyKoef = 1 - (absoluteTorgCenaDifference / torgCenaAfterAfter);


            });

            // общий процент
            AssessmentGround.OG_GeneralProcent = AnalogueGrounds[0].AG_VesovoyKoef + AnalogueGrounds[1].AG_VesovoyKoef + AnalogueGrounds[2].AG_VesovoyKoef;

            // весовой коэффициент
            AnalogueGrounds[0].AG_VesovoyKoef2 = AnalogueGrounds[0].AG_VesovoyKoef / ПолучитьКорректировку(AssessmentGround.OG_GeneralProcent);
            AnalogueGrounds[1].AG_VesovoyKoef2 = AnalogueGrounds[1].AG_VesovoyKoef / ПолучитьКорректировку(AssessmentGround.OG_GeneralProcent);
            AnalogueGrounds[2].AG_VesovoyKoef2 = AnalogueGrounds[2].AG_VesovoyKoef / ПолучитьКорректировку(AssessmentGround.OG_GeneralProcent);
            // цена после весовых коэффициентов
            AnalogueGrounds[0].AG_VesovoyKoef2CenaAfter = AnalogueGrounds[0].AG_VesovoyKoef2 * ПолучитьКорректировку(AnalogueGrounds[0].AG_NaMashtabCenaAfter2);
            AnalogueGrounds[1].AG_VesovoyKoef2CenaAfter = AnalogueGrounds[1].AG_VesovoyKoef2 * ПолучитьКорректировку(AnalogueGrounds[1].AG_NaMashtabCenaAfter2);
            AnalogueGrounds[2].AG_VesovoyKoef2CenaAfter = AnalogueGrounds[2].AG_VesovoyKoef2 * ПолучитьКорректировку(AnalogueGrounds[2].AG_NaMashtabCenaAfter2);
            // цена за квадратный метр земельного участка
            AssessmentGround.OG_CenaKvm = AnalogueGrounds[0].AG_VesovoyKoef2CenaAfter + AnalogueGrounds[1].AG_VesovoyKoef2CenaAfter + AnalogueGrounds[2].AG_VesovoyKoef2CenaAfter;
            // цена весь земельный участок
            AssessmentGround.OG_CenaVse = AssessmentGround.OG_CenaKvm * AssessmentGround.OG_Ploshad;

            //если надо возвести в степень числа
            //AnalogueGrounds[0].AG_NaMashtabCenaAfter = ВозвестиВ(степеньЧисла: 2,
            //                                                     аргумент: AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[0].AG_Ploshad));

            //AnalogueGrounds[0].AG_NaMashtabCenaAfter = Math.Pow(AssessmentGround.OG_Ploshad / ПолучитьКорректировку(AnalogueGrounds[0].AG_Ploshad), nonSenseCoefficient);
            //AssessmentGround.OG_PriceKvmAfter = AssessmentGround.OG_CenaKvm * nonSenseCoefficient;
            //AssessmentGround.OG_C_TotalPrice = AssessmentGround.OG_PriceKvmAfter * AssessmentObject.OH_Ploshad * nonSenseCoefficient;



            //// корректировка на торг
            //AnalogueGrounds[0].AG_CenaProdazhiKvm = AnalogueGrounds[0].AG_CenaProdazhiKvm * (AnalogueGrounds[0].AG_KorTorg == 0 ? 1 : AnalogueGrounds[0].AG_KorTorg);
            //AnalogueGrounds[1].AG_CenaProdazhiKvm = AnalogueGrounds[1].AG_CenaProdazhiKvm * (AnalogueGrounds[1].AG_KorTorg == 0 ? 1 : AnalogueGrounds[1].AG_KorTorg);
            //AnalogueGrounds[2].AG_CenaProdazhiKvm = AnalogueGrounds[2].AG_CenaProdazhiKvm * (AnalogueGrounds[2].AG_KorTorg == 0 ? 1 : AnalogueGrounds[2].AG_KorTorg);
            //// новая цена (атрибута в бд нет)
            //AnalogueGrounds[0].AG_C_PriceAfter = AnalogueGrounds[0].AG_CenaProdazhiKvm;
            //AnalogueGrounds[1].AG_C_PriceAfter = AnalogueGrounds[1].AG_CenaProdazhiKvm;
            //AnalogueGrounds[2].AG_C_PriceAfter = AnalogueGrounds[2].AG_CenaProdazhiKvm;
            //// расчёт финальной стоимости земельного участка (атрибута в бд нет)
            //const float nonSenseCoefficient = 1.05f;
            //AssessmentGround.OG_CenaKvm = AssessmentGround.OG_CenaVse / AssessmentGround.OG_Ploshad;
            //AssessmentGround.OG_PriceKvmAfter = AssessmentGround.OG_CenaKvm * nonSenseCoefficient;
            //AssessmentGround.OG_C_TotalPrice = AssessmentGround.OG_PriceKvmAfter * AssessmentObject.OH_Ploshad * nonSenseCoefficient;
            //// корректировка на права
            //AnalogueGrounds[0].Torg = AnalogueGrounds[0].AG_CenaProdazhiKvm * (AnalogueGrounds[0].AG_KorNaDatuProdazhi == 0 ? 1 : AnalogueGrounds[0].AG_KorNaDatuProdazhi); 
            #endregion
            #region allCalculation
            // стоимость объекта оценки все
            AssessmentAll.OA_CenaAll = AssessmentGround.OG_CenaVse + AssessmentObject.OH_CenaVse;
            // стоимость за квадратный метр
            AssessmentAll.OA_CenaAllKvm = AssessmentAll.OA_CenaAll / AssessmentObject.OH_Ploshad;
            // 
            AssessmentAll.OA_PloshadZemli = AssessmentGround.OG_Ploshad;
            AssessmentAll.OA_PloshadDom = AssessmentObject.OH_Ploshad;
            AssessmentAll.OA_CenaDomVse = AssessmentObject.OH_CenaVse;
            AssessmentAll.OA_CenaDomKvm = AssessmentObject.OH_CenaKvm;
            AssessmentAll.OA_CenaZemliKvm = AssessmentGround.OG_CenaKvm;
            AssessmentAll.OA_CenaZemliVse = AssessmentGround.OG_CenaVse;
            AssessmentAll.OA_Adress = AssessmentObject.OH_Adress;
            AssessmentAll.OA_Photo = AssessmentObject.OH_Photo;
            #endregion

            UpdateHousePercentOfCompletion();
            UpdateGroundPercentOfCompletion();
            if (IsAbleToPerformRequest())
            {
                SetStatusOfRequest();
            }
            try
            {
                AppData.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            IsUpdating = false;
        }

        public float? ПолучитьАбсолютноеЗначение(float? аргумент)
        {
            if (аргумент == null) return аргумент;
            return (float?)Math.Abs((double)аргумент);
        }

        private float? ВозвестиВ(int степеньЧисла, float? аргумент)
        {
            if (аргумент == null) return аргумент;
            return (float?)Math.Pow((double)аргумент, степеньЧисла);
        }

        private float? ВозвестиВСтепень(float? переменной, float? аргумент)
        {
            if (переменной == null) return аргумент;
            if (аргумент == null) return аргумент;

            return (float?)Math.Pow((double)аргумент, (double)переменной);
        }

        /// <summary>
        /// Вычисляет значение для индексов 0, 1 и 2, 
        /// используя единственную формулу.
        /// </summary>
        /// <param name="callback">Функция, вычисляющее значение по формуле.</param>
        /// <example>
        /// </example>
        private void Execute(Action<int> callback)
        {
            for (int i = 0; i < 3; i++)
            {
                callback(i);
            }
        }

        /// <summary>
        /// Возвращает значение корректировки, 
        /// которая была передана аргументом, 
        /// или 1, если корректировка была равна 0 или не определена.
        /// </summary>
        /// <param name="value">Корректировка для преобразования.</param>
        /// <returns>Значение корректировки, 
        /// которая была передана аргументом, 
        /// или 1, если корректировка была равна 0 или не определена.</returns>
        public float? ПолучитьКорректировку(float? value)
        {
            if (!value.HasValue || value == 0)
            {
                return 1;
            }
            else
            {
                return value;
            }
        }

        public void UpdateGroundPercentOfCompletion()
        {
            IEnumerable<ListViewItem> analogueGroundsDependencyObjects =
                App.WorkOrdinary.GroundCalculationView.Find<ListViewItem>();
            List<TextBox> analogueGroundTextBoxes = new List<TextBox>();
            List<ComboBox> analogueGroundComboBoxes = new List<ComboBox>();
            foreach (ListViewItem dependencyObject in analogueGroundsDependencyObjects)
            {
                analogueGroundTextBoxes.AddRange(
                   LogicalChildrenFinder.Find<TextBox>(dependencyObject));
                analogueGroundComboBoxes.AddRange(
                   LogicalChildrenFinder.Find<ComboBox>(dependencyObject));
            }

            IEnumerable<TextBox> textBoxes = App.WorkOrdinary.GroundSecondColumn
                .Find<TextBox>()
                .Union(analogueGroundTextBoxes)
                .Where(tb =>
                {
                    if (tb.TemplatedParent == null)
                    {
                        return true;
                    }
                    return tb.TemplatedParent.GetType() != typeof(ComboBox);
                });
            IEnumerable<ComboBox> comboBoxes = App.WorkOrdinary.GroundSecondColumn
                .Find<ComboBox>()
                .Union(analogueGroundComboBoxes);
            int filledControlsCount = textBoxes.Count(tb =>
            {
                return !string.IsNullOrWhiteSpace(tb.Text);
            })
                + comboBoxes.Count(cb => cb.SelectedItem != null);
            int totalControlsCount = textBoxes.Count()
                + comboBoxes.Count();

            GroundPercentOfCompletion = 1.0 * filledControlsCount
                / totalControlsCount
                * 100;

            if (GroundPercentOfCompletion > 100)
            {
                GroundPercentOfCompletion = 100;
            }
        }

        private void SetStatusOfRequest()
        {
            Zayavka.Z_StatusId = ZayavkaStatuses.Vypolnena;
            try
            {
                AppData.Context.SaveChanges();
                MessageBox.Show("Заявка выполнена. Её статус изменён");
            }
            catch (Exception)
            {
                MessageBox.Show("Статус заявки не изменён, " +
                    "но она выполнена. " +
                    "Проверьте подключение к базе данных");
            }
        }

        private bool IsAbleToPerformRequest()
        {
            return GroundPercentOfCompletion == 100
                && HousePercentOfCompletion == 100
                && Zayavka.Klient.IsInflated
                && Zayavka.Z_StatusId == ZayavkaStatuses.V_Obrabotke;
        }

        /// <summary>
        /// Обновляет процент выполнения заявки дома. 
        /// В случае достижения ста процентов обновляет 
        /// статус заявки на <see langword="Выполнено"/>.
        /// </summary>
        public void UpdateHousePercentOfCompletion()
        {
            IEnumerable<ListViewItem> analogueDependencyObjects =
            App.WorkOrdinary.AnalogueHouseCalculationView.Find<ListViewItem>();
            List<TextBox> analogueTextBoxes = new List<TextBox>();
            List<ComboBox> analogueComboBoxes = new List<ComboBox>();
            foreach (ListViewItem dependencyObject in analogueDependencyObjects)
            {
                analogueTextBoxes.AddRange(
                   LogicalChildrenFinder.Find<TextBox>(dependencyObject));
                analogueComboBoxes.AddRange(
                   LogicalChildrenFinder.Find<ComboBox>(dependencyObject));
            }

            IEnumerable<TextBox> textBoxes = App.WorkOrdinary.HouseSecondColumn
                .Find<TextBox>()
                .Union(analogueTextBoxes)
                .Where(tb =>
                {
                    if (tb.TemplatedParent == null)
                    {
                        return true;
                    }
                    return tb.TemplatedParent.GetType() != typeof(ComboBox);
                });
            IEnumerable<ComboBox> comboBoxes = App.WorkOrdinary.HouseSecondColumn
                .Find<ComboBox>()
                .Union(analogueComboBoxes);
            int filledControlsCount = textBoxes.Count(tb =>
            {
                return !string.IsNullOrWhiteSpace(tb.Text);
            })
                + comboBoxes.Count(cb => cb.SelectedItem != null);
            int totalControlsCount = textBoxes.Count()
                + comboBoxes.Count();

            HousePercentOfCompletion = 1.0 * filledControlsCount
                / totalControlsCount
                * 100;

            if (HousePercentOfCompletion > 100)
            {
                HousePercentOfCompletion = 100;
            }
        }

        public Zayavka Zayavka
        {
            get => zayavka;
            set => SetProperty(ref zayavka, value);
        }


        private ObjectAssessmentHouse assessmentObject;
        private ObservableCollection<AnalogiHouse> analogueHouses;

        public ObjectAssessmentHouse AssessmentObject
        {
            get => assessmentObject;
            set => SetProperty(ref assessmentObject, value);
        }
        public ObjectAssessmentGround AssessmentGround
        {
            get => assessmentGround;
            set => SetProperty(ref assessmentGround, value);
        }
        public ObjectAssessmentAll AssessmentAll
        {
            get => assessmentAll;
            set => SetProperty(ref assessmentAll, value);
        }
        public ObservableCollection<AnalogiHouse> AnalogueHouses
        {
            get => analogueHouses;
            set => SetProperty(ref analogueHouses, value);
        }

        private Command changeAssessmentObjectPictureCommand;

        public ICommand ChangeAssessmentObjectPictureCommand
        {
            get
            {
                if (changeAssessmentObjectPictureCommand == null)
                {
                    changeAssessmentObjectPictureCommand =
                        new Command(ChangeAssessmentObjectPicture);
                }

                return changeAssessmentObjectPictureCommand;
            }
        }

        private bool? GetImageFromWindowsOS(out string fileName)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите фото"
            };
            bool? result = openFileDialog.ShowDialog();
            fileName = openFileDialog.FileName;
            return result;
        }

        private void ChangeAssessmentObjectPicture()
        {
            bool? result = GetImageFromWindowsOS(out string fileName);
            if (result.HasValue && result.Value)
            {
                AssessmentObject.OH_Photo = File
                    .ReadAllBytes(fileName);
                MessageBox.Show("Фото объекта оценки изменено");
            }
        }


        private ObservableCollection<string> additionalBuildingTypes;

        public ObservableCollection<string> AdditionalBuildingTypes
        {
            get => additionalBuildingTypes;
            set => SetProperty(ref additionalBuildingTypes, value);
        }
        public ObservableCollection<string> TypeBlagoustroistva { get; private set; }
        public ObservableCollection<string> TypeMebel { get; private set; }
        public ObservableCollection<string> TypeMaterialSten { get; private set; }
        public ObservableCollection<string> TypeOtoplenia { get; private set; }
        public ObservableCollection<string> TypeVoda { get; private set; }
        public ObservableCollection<string> TypeKanalizacia { get; private set; }
        public ObservableCollection<string> TypeFizSostoyanie { get; private set; }
        public ObservableCollection<string> TypeVnutrOtdelki { get; private set; }
        public ObservableCollection<string> TypeNarugOtdelki { get; private set; }

        private Command changeAnalogueHousePictureCommand;

        public ICommand ChangeAnalogueHousePictureCommand
        {
            get
            {
                if (changeAnalogueHousePictureCommand == null)
                {
                    changeAnalogueHousePictureCommand
                        = new Command(ChangeAnalogueHousePicture);
                }

                return changeAnalogueHousePictureCommand;
            }
        }

        private void ChangeAnalogueHousePicture(object parameter)
        {
            bool? result = GetImageFromWindowsOS(out string fileName);
            if (result.HasValue && result.Value)
            {
                (parameter as AnalogiHouse).AH_Photo = File
                   .ReadAllBytes(fileName);
                MessageBox.Show("Фото аналога изменено");
            }
        }

        private double percentOfCompletion;
        private ObservableCollection<AnalogiGround> analogueGrounds;

        public double HousePercentOfCompletion
        {
            get => percentOfCompletion;
            set => SetProperty(ref percentOfCompletion, value);
        }
        public bool IsUpdating { get; private set; }
        public ObservableCollection<AnalogiGround> AnalogueGrounds
        {
            get => analogueGrounds;
            set => SetProperty(ref analogueGrounds, value);
        }

        private double groundPercentOfCompletion;

        public double GroundPercentOfCompletion
        {
            get => groundPercentOfCompletion;
            set => SetProperty(ref groundPercentOfCompletion, value);
        }

        private Command changeAssessmentGroundPictureCommand;

        public ICommand ChangeAssessmentGroundPictureCommand
        {
            get
            {
                if (changeAssessmentGroundPictureCommand == null)
                {
                    changeAssessmentGroundPictureCommand = new Command(ChangeAssessmentGroundPicture);
                }

                return changeAssessmentGroundPictureCommand;
            }
        }

        private void ChangeAssessmentGroundPicture()
        {
            bool? result = GetImageFromWindowsOS(out string fileName);
            if (result.HasValue && result.Value)
            {
                AssessmentGround.OG_Photo = File
                    .ReadAllBytes(fileName);
                MessageBox.Show("Фото объекта оценки изменено");
            }
        }

        private ObservableCollection<string> typesOfGround;

        public ObservableCollection<string> TypesOfGround
        {
            get => typesOfGround;
            set => SetProperty(ref typesOfGround, value);
        }
        public ObservableCollection<string> AllowedUsages { get; set; }
        public ObservableCollection<string> VozmognostPodkluchenia { get; private set; }

        private Command changeAnalogueGroundPictureCommand;
        private ObjectAssessmentGround assessmentGround;
        private ObjectAssessmentAll assessmentAll;

        public ICommand ChangeAnalogueGroundPictureCommand
        {
            get
            {
                if (changeAnalogueGroundPictureCommand == null)
                {
                    changeAnalogueGroundPictureCommand = new Command(ChangeAnalogueGroundPicture);
                }

                return changeAnalogueGroundPictureCommand;
            }
        }

        private void ChangeAnalogueGroundPicture(object parameter)
        {
            bool? result = GetImageFromWindowsOS(out string fileName);
            if (result.HasValue && result.Value)
            {
                (parameter as AnalogiGround).AG_Photo = File
                   .ReadAllBytes(fileName);
                MessageBox.Show("Фото земельного аналога изменено");
            }
        }
    }
}
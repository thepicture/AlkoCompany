﻿using AlkoCompanyNew.Commands;
using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
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
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            AssessmentObject = zayavka.ObjectAssessmentHouse;
            AnalogueHouses = new ObservableCollection<AnalogiHouse>
                (AssessmentObject.AnalogiHouse);
            foreach (AnalogiHouse analogueHouse in AnalogueHouses)
            {
                analogueHouse.NumberOfAnalogue
                    = AnalogueHouses.IndexOf(analogueHouse) + 1;
            }
            AssessmentObject.PropertyChanged += OnAssessmentObjectChanged;
            foreach (AnalogiHouse analogueHouse in AnalogueHouses)
            {
                analogueHouse.PropertyChanged += OnAssessmentObjectChanged;
            }
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
        private void OnAssessmentObjectChanged(object sender,
                                               PropertyChangedEventArgs e)
        {
            #region calculation
            //AssessmentObject.OH_C_Correction = AssessmentObject
            //    .AnalogiHouse
            //    .Average(ah => ah.AH_Correction);

            // цена кв.м
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiVse / AnalogueHouses[0].AH_Ploshad;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiVse / AnalogueHouses[1].AH_Ploshad;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiVse / AnalogueHouses[2].AH_Ploshad;
            // корректировка на торг
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorTorg;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorTorg;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorTorg;
            // корректировка на дату продажи
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaDatuProdazhi;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaDatuProdazhi;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaDatuProdazhi;
            // корректировка на месторасположение
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaMesto;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaMesto;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaMesto;
            // корректировка на дополнительные постройки 
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaDopPostroyki;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaDopPostroyki;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaDopPostroyki;
            // корректировка на благоустройство участка
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaBlagoustroistvo;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaBlagoustroistvo;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaBlagoustroistvo;
            // корректировка на мебель
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaMebel;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaMebel;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaMebel;
            // корректировка на этажность
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaEtage;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaEtage;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaEtage;
            // корректировка на материал стен
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaMaterialSten;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaMaterialSten;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaMaterialSten;
            // корректировка на тип отопления
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaOtoplenie;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaOtoplenie;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaOtoplenie;
            // корректировка на тип воды
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaVoda;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaVoda;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaVoda;
            // корректировка на тип канализации
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaKanalizacia;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaKanalizacia;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaKanalizacia;
            // корректировка на физическое состояние
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaFizSostoyanie;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaFizSostoyanie;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaFizSostoyanie;
            // корректировка на внутреннюю отделку
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaVnutrenyuOtdelky;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaVnutrenyuOtdelky;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaVnutrenyuOtdelky;
            // корректировка на наружную отделку
            AnalogueHouses[0].AH_CenaProdazhiKvm = AnalogueHouses[0].AH_CenaProdazhiKvm * AnalogueHouses[0].AH_KorNaNarugnyuOtdelky;
            AnalogueHouses[1].AH_CenaProdazhiKvm = AnalogueHouses[1].AH_CenaProdazhiKvm * AnalogueHouses[1].AH_KorNaNarugnyuOtdelky;
            AnalogueHouses[2].AH_CenaProdazhiKvm = AnalogueHouses[2].AH_CenaProdazhiKvm * AnalogueHouses[2].AH_KorNaNarugnyuOtdelky;
            #endregion

            try
            {
                AppData.Context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                _ = MessageBox.Show("Фото объекта оценки изменено");
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
                _ = MessageBox.Show("Фото аналога изменено");
            }
        }
    }
}
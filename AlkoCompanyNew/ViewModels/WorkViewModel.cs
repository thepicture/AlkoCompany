using AlkoCompanyNew.Commands;
using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                    "отсутствуют",
                    "присутствуют"
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
            AssessmentObject.OH_C_Correction = AssessmentObject
                .AnalogiHouse
                .Average(ah => ah.AH_Correction);
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

        private void ChangeAssessmentObjectPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите фото объекта оценки"
            };
            bool? result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                AssessmentObject.OH_Photo = File
                    .ReadAllBytes(openFileDialog.FileName);
                _ = MessageBox.Show("Изображение фото объекта оценки " +
                    "изменено");
            }
        }


        private ObservableCollection<string> additionalBuildingTypes;

        public ObservableCollection<string> AdditionalBuildingTypes
        {
            get => additionalBuildingTypes;
            set => SetProperty(ref additionalBuildingTypes, value);
        }
    }
}
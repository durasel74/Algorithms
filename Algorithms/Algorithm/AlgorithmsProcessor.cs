using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
    /// <summary>
    /// Главный механизм запуска алгоритмов. Управляет временем выполнения.
    /// </summary>
	public class AlgorithmsProcessor : INotifyPropertyChanged
    {
        private const int timePause = 100;
        private const int countFromLimit = 1;
        private const int countToLimit = 500;
        private readonly ObservableCollection<Number> array;

        private TimeSetting timeSetting;
        private int currentTimeSpeed;
        private bool isRunning;
        private bool isComplite;
        private bool isPause;
        private int countFrom, countTo;
        private Number selectedElement;
        private int attempt;
        private string algorithmInfo;
        private bool showInfo;
        private int sequenceInterval;

        public ObservableCollection<Number> Array { get { return array; } }

        /// <summary>
        /// Запущен ли в данный момент алгоритм.
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        /// <summary>
        /// Завершил ли работу алгоритм.
        /// </summary>
        public bool IsComplite
        {
            get { return isComplite; }
            set
            {
                isComplite = value;
                OnPropertyChanged("IsComplite");
            }
        }

        /// <summary>
        /// Приостановлен ли в данный момент алгоритм.
        /// </summary>
        public bool IsPause
        {
            get { return isPause; }
            set
            {
                isPause = value;
                OnPropertyChanged("IsPause");
            }
        }

        /// <summary>
        /// Граница начального значения массива.
        /// </summary>
        public int CountFrom
        {
            get { return countFrom; }
            set
            {
                countFrom = CountLimitCheck(value);
                if (countFrom > CountTo)
                    countFrom = CountTo;
                if (RestartEventHandler != null)
                    Restart();
				OnPropertyChanged("CountFrom");
			}
		}

        /// <summary>
        /// Граница последнего значения массива.
        /// </summary>
        public int CountTo
        {
            get { return countTo; }
            set
            {
                countTo = CountLimitCheck(value);
                if (countTo < CountFrom)
					countTo = CountFrom;
				if (RestartEventHandler != null)
                    Restart();
                OnPropertyChanged("CountTo");
            }
        }

        /// <summary>
        /// Элемент, обрабатываемый алгоритмом в данный момент.
        /// </summary>
        public Number SelectedElement
        {
            get { return selectedElement; }
            set
            {
                selectedElement = value;
                OnPropertyChanged("SelectedElement");
            }
        }

        /// <summary>
        /// Шаг или итерация выполнения алгоритма.
        /// </summary>
        public int Attempt 
        { 
            get { return attempt; } 
            set
            {
                attempt = value;
                OnPropertyChanged("Attempt");
			}
        }

        /// <summary>
        /// Информация о работе алгоритма.
        /// </summary>
        public string AlgorithmInfo
        {
            get { return algorithmInfo; }
            set
            {
                algorithmInfo = value;
                OnPropertyChanged("AlgorithmInfo");
            }
        }

        /// <summary>
        /// Нужно ли показывать информацию о работе алгоритма.
        /// </summary>
        public bool ShowInfo
        {
            get { return showInfo; }
            set
            {
                showInfo = value;
                OnPropertyChanged("ShowInfo");
			}
		}

        /// <summary>
        /// Интервал между выводимыми числами.
        /// </summary>
        public int SequenceInterval
        { 
            get { return sequenceInterval; }
            set
            {
                sequenceInterval = value;
                Restart();
                OnPropertyChanged("SequenceInterval");
			}
        }

        // Событие запуска алгоритма
        public delegate void AlgorithmHundler();
        public event AlgorithmHundler AlgorithmEventHandler;

        // Событие перезапуска алгоритма
        public delegate void AlgorithmRestart();
        public event AlgorithmRestart RestartEventHandler;

        public AlgorithmsProcessor()
        {
            array = new ObservableCollection<Number>();
            CountTo = 100;
            CountFrom = 1;
            SelectedElement =  null;
            IsComplite = true;
            IsPause = true;
            isRunning = false;
            ShowInfo = true;
            sequenceInterval = 1;

            RestartEventHandler += RestartAlgorithm;
            UpdateInfo();
            Restart();
        }

        /// <summary>
        /// Устанавливает профиль времени для алгоритма.
        /// </summary>
        /// <param name="timeProfile">Один из вариантов профиля времени</param>
        public void SetTimeProfile(TimeProfile timeProfile)
        {
             switch (timeProfile)
             {
                case TimeProfile.Search:
                    timeSetting = new TimeSetting(50000000, 5000000, 500000);
                    break;
                case TimeProfile.Sorting:
                    timeSetting = new TimeSetting(100000, 10000, 1000);
                    break;
            }
		}

        /// <summary>
        /// Устанавливает скорость, с которой будет выполняться алгоритм.
        /// </summary>
        /// <param name="timeSwitch">Вариант скорости выполнения алгоритма.</param>
        public void SetTimeSpeed(TimeSwitch timeSwitch)
        {
            switch (timeSwitch)
            {
                case TimeSwitch.Slow:
                    currentTimeSpeed = timeSetting.Slow;
                    break;
                case TimeSwitch.Medium:
                    currentTimeSpeed = timeSetting.Medium;
                    break;
                case TimeSwitch.Fast:
                    currentTimeSpeed = timeSetting.Fast;
                    break;
            }
		}

        /// <summary>
        /// Нормализация размера массива относительно максимально возможного. 
        /// (Не изменяет значение, если границы не были превышены.)
        /// </summary>
        /// <param name="value">Нормализуемое значение.</param>
        /// <returns>Нормализованное значение.</returns>
        public int CountLimitCheck(int value)
        {
            if (value < countFromLimit)
                return countFromLimit;
            else if (value > countToLimit)
                return countToLimit;
            else
                return value;
        }

        /// <summary>
        /// Асинхронный запуск алгоритма.
        /// </summary>
        public async void Run()
        {
            if (!isRunning)
            {
                Restart();

                isRunning = true;
                IsComplite = false;
                IsPause = false;
                await Task.Run(() => StartAlgorithm());
                IsPause = true;
                IsComplite = true;
                isRunning = false;
                UpdateInfo();
            }
        }

        /// <summary>
        /// Перезапускает алгоритм.
        /// </summary>
        public void Restart()
        {
            Close();
            RestartEventHandler.Invoke();
            UpdateInfo();
		}

        /// <summary>
        /// Останавливает работу алгоритма.
        /// </summary>
        public void Close()
        {
            isComplite = true;
            isPause = true;
        }

        /// <summary>
        /// Управляет скоростью выполнения алгоритма и паузами.
        /// </summary>
        /// <returns>Был ли выход непринужденным</returns>
        public bool TimeManagement()
        {
            UpdateInfo();

			while (isPause)
			{
				if (isComplite) return false;
				Thread.Sleep((int)timePause);
				UpdateInfo();
			}

			if (isComplite) return false;
            var now = DateTime.Now;
            while (DateTime.Now < now.AddTicks(currentTimeSpeed)) { }
			if (isComplite) return false;

            return true;
        }

        /// <summary>
        /// Получение текущего состояния работы алгоритма.
        /// </summary>
        /// <returns>Строковое состояние</returns>
        public string GetState()
        {
            if (!IsRunning)
                return "Готово";
            else if (IsPause)
                return "Пауза";
            else
                return "Запущено";
        }

        /// <summary>
        /// Обновление информации о работе алгоритма.
        /// </summary>
        public virtual void UpdateInfo()
        {
            string info = "" +
            $"Состояние: {GetState()}\n" +
            $"Количество элементов: {Array.Count}\n" +
            $"Попытка: {Attempt}";
            AlgorithmInfo = info;
        }

		/// <summary>
		/// Обновляет массив на основе изменений.
		/// </summary>
		protected void UpdateArray()
        {
            Array.Clear();
            for (int i = countFrom; i < countTo + 1; i++)
            {
                Array.Add(new Number(i * sequenceInterval));
            }
        }

        // Вызывает событие запуска алгоритма
        private void StartAlgorithm()
        {
            AlgorithmEventHandler.Invoke();
        }

        // Сброс основной части алгоритма
        private void RestartAlgorithm()
        {
            Attempt = 0;
            UpdateArray();
            SelectedElement = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

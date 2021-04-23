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
        private const int timeStepLimit = 206;
        private const int timeStep = 5;
        private const int timePause = 100;
        private const int countFromLimit = -500;
        private const int countToLimit = 500;
        private readonly ObservableCollection<int> array;

        private bool isRunning;
        private bool isComplite;
        private bool isPause;
        private double speed;
        private int countFrom, countTo;
        private int selectedElement;
        private int attempt;
        private string algorithmInfo;
        private bool showInfo;

        public ObservableCollection<int> Array { get { return array; } }

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
        /// Скорость выполнения алгоритма.
        /// </summary>
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
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
                if (value > CountTo)
                    countFrom = CountTo;
                else
                    countFrom = value;

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
                if (value < CountFrom)
					countTo = CountFrom;
				else
					countTo = value;

				if (RestartEventHandler != null)
                    Restart();
                OnPropertyChanged("CountTo");
            }
        }

        /// <summary>
        /// Элемент, обрабатываемый алгоритмом в данный момент.
        /// </summary>
        public int SelectedElement
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

        // Событие запуска алгоритма
        public delegate void AlgorithmHundler();
        public event AlgorithmHundler AlgorithmEventHandler;

        // Событие перезапуска алгоритма
        public delegate void AlgorithmRestart();
        public event AlgorithmRestart RestartEventHandler;

        public AlgorithmsProcessor()
        {
            array = new ObservableCollection<int>();
            CountTo = 100;
            CountFrom = 1;
            SelectedElement = CountFrom - 1;
            IsComplite = true;
            IsPause = true;
            isRunning = false;
            Speed = 90;
            ShowInfo = true;

            RestartEventHandler += RestartAlgorithm;
            UpdateInfo();
            Restart();
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
            int stepCount = (int)(timeStepLimit - speed * 2);

            for (int i = 0; i < stepCount; i++)
            {
                while (isPause)
                {
                    if (isComplite) return false;
                    Thread.Sleep((int)timePause);
                    UpdateInfo();
                }
                if (isComplite) return false;
                Thread.Sleep(timeStep);
            }
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
        /// Безопасное получение элемента списка.
        /// </summary>
        /// <returns></returns>
        public bool GetArrayElement(int index, ref int element)
        {
            bool result = false;
            if (Array.Count != 0)
            {
                element = Array[index];
                result = true;
            }
            return result;
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
            SelectedElement = countFromLimit - 1;
        }

        // Очищает и заполняет массив по новой
        private void UpdateArray()
        {
            array.Clear();
            for (int i = countFrom; i < countTo + 1; i++)
                array.Add(i);
            OnPropertyChanged("Array");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

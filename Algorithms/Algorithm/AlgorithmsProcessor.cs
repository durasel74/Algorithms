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
        private ObservableCollection<int> array;
        private int countFrom, countTo;
        private int selectedElement;
        private bool isComplite;
        private bool isPause;
        private bool isRunning;
        private double speed;
        private int attempt;
        private bool showInfo;
        private string algorithmName;
        private string algorithmInfo;

        public ObservableCollection<int> Array { get { return array; } }
        public int CountFrom
        {
            get { return countFrom; }
            set
            {
                countFrom = value;
                OnPropertyChanged("CountFrom");
                UpdateArray();
			}
		}
        public int CountTo
        {
            get { return countTo; }
            set
            {
                countTo = value;
                OnPropertyChanged("countTo");
                UpdateArray();
            }
        }
        public int SelectedElement
        {
            get { return selectedElement; }
            set
            {
                selectedElement = value;
                OnPropertyChanged("SelectedElement");
            }
        }
		public bool IsComplite
		{
			get { return isComplite; }
		}
		public bool IsRunning
        {
            get { return isRunning; }
        }
        public bool IsPause
        {
            get { return isPause; }
            set
            {
                isPause = value;
                OnPropertyChanged("IsPause");
			}
		}
        public double Speed
        {
            get { return speed; }
            set
            {
                double temp = Math.Round(value, 1);
                if (temp <= 0)
                    speed = 1;
                else if (temp > 100)
                    speed = 100;
                else
                    speed = temp;
                OnPropertyChanged("Speed");
            }
        }
        public int Attempt 
        { 
            get { return attempt; } 
            set
            {
                attempt = value;
                OnPropertyChanged("Attempt");
			}
        }
        public bool ShowInfo
        {
            get { return showInfo; }
            set
            {
                showInfo = value;
                OnPropertyChanged("ShowInfo");
			}
		}
        public string AlgorithmName
        {
            get { return algorithmName; }
            set
            {
                algorithmName = value;
                OnPropertyChanged("AlgorithmName");
			}
		}
        public string AlgorithmInfo
        {
            get { return algorithmInfo; }
            set
            {
                algorithmInfo = value;
                OnPropertyChanged("AlgorithmInfo");
            }
        }

        public delegate bool AlgorithmHundler();
        public event AlgorithmHundler _algorithmEventHandler;
        public event AlgorithmHundler AlgorithmEventHandler
        {
            add
            {
                _algorithmEventHandler += value;
                UpdateInfo();
            }
            remove
            {
                _algorithmEventHandler -= value;
            }
        }

        public delegate void AlgorithmRestart();
        public event AlgorithmRestart RestartEventHandler;

        public AlgorithmsProcessor()
        {
            array = new ObservableCollection<int>();
            CountFrom = 1;
            CountTo = 100;
            SelectedElement = CountFrom - 1;
            isComplite = true;
            IsPause = true;
            isRunning = false;
            Speed = 15;
            ShowInfo = true;
            RestartEventHandler += RestartAlgorithm;
            UpdateInfo();
        }

        /// <summary>
        /// Асинхронный запуск алгоритма.
        /// </summary>
        public async void Run()
        {
            if (!isRunning)
            {
                RestartEvent();

                isRunning = true;
                isComplite = false;
                IsPause = false;
                await Task.Run(() => StartAlgorithm());
                IsPause = true;
                isComplite = true;
                isRunning = false;
                UpdateInfo();
            }
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
        /// Запускает событие перезагрузки.
        /// </summary>
        public void RestartEvent()
        {
            RestartEventHandler.Invoke();
        }

        private void StartAlgorithm()
        {
            bool again;
            while (!isComplite)
            {
                UpdateInfo();
                Thread.Sleep((int)(5000 / speed));
                if (isPause)
                {
                    if (isComplite) break;
                    Thread.Sleep(100);
                    continue;
                }

                again = _algorithmEventHandler.Invoke();
                if (!again) return;
            }
        }
        private void UpdateArray()
        {
            array.Clear();
            for (int i = countFrom; i < countTo + 1; i++)
                array.Add(i);
            OnPropertyChanged("Array");
        }
        private void RestartAlgorithm()
        {
            Attempt = 0;
            UpdateArray();
            SelectedElement = countFrom - 1;
            UpdateInfo();
        }
        private void UpdateInfo()
        {
			string info = "" +
            $"Алгоритм: {AlgorithmName}\n" +
            $"Состояние: {GetState()}\n" + 
            $"Количество элементов: {Array.Count}\n" +
            $"Попытка: {Attempt}"
            ;
            AlgorithmInfo = info;
		}
        private string GetState()
        {
            if (!IsRunning)
                return "Готово";
            else if (!IsPause)
                return "Запущено";
            else if (IsPause)
                return "Пауза";
            else
                return "Ошибка";
		}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

using System;

namespace Algorithms.Algorithm.EasySortings
{
	public class EasySortings : AlgorithmsProcessor
	{
		private Random random = new Random();

		private string algorithmName = "Сортировка";
		private int sequenceInterval = 4;


		public int Low { get; private set; }
		public int High { get; private set; }


		public int SequenceInterval
		{ 
			get { return sequenceInterval; }
			set
			{
				sequenceInterval = value;
				OnPropertyChanged("SequenceInterval");
			}
		}


		public EasySortings()
		{
			AlgorithmEventHandler += Sort;
			RestartEventHandler += RestartSorting;
			SetTimeProfile(TimeProfile.Sorting);

			//SetTimeSpeed(TimeSwitch.Slow);
			SetTimeSpeed(TimeSwitch.Medium);
			//SetTimeSpeed(TimeSwitch.Fast);

			Low = 0;
			High = Array.Count - 1;
		}

		/// <summary>
		/// Представляет свою версию обновления информации для алгоритма. 
		///	(Простая сортировка)
		/// </summary>
		public override void UpdateInfo()
		{
			string info = "" +
			$"Алгоритм: {algorithmName}\n" +
			$"Состояние: {GetState()}\n" +
			$"Количество элементов: {Array.Count}\n" +
			$"Попытка: {Attempt}\n";
			AlgorithmInfo = info;
		}

		/// <summary>
		/// Представляет свою версию обновления массива. (Простая сортировка)
		/// </summary>
		protected override void UpdateArray()
		{
			Array.Clear();
			for (int i = CountFrom; i < CountTo + 1; i += 1)
			{
				Array.Add(new Number(i * sequenceInterval));
			}
		}

		

		private void Sort()
		{
			BubbleSort();


		}


		private void BubbleSort()
		{
			Number currentNumber;
			Number secondNumber;

			for (int i = 0; i < Array.Count; i++)
			{
				for (int j = 0; j < Array.Count - i - 1; j++)
				{
					Attempt += 1;

					currentNumber = Array[j];
					secondNumber = Array[j + 1];

					SelectedElement = secondNumber;

					if (currentNumber.Value > secondNumber.Value)
					{
						SwapElementsInArray(j, j + 1);
						if (!TimeManagement()) return;
					}
				}
				High = Array.Count - i - 3;
			}
			SelectedElement = null;
		}

		// Перемешивает массив
		private void ShuffleArray()
		{
			int j;
			for (int i = Array.Count - 1; i >= 1; i--)
			{
				j = random.Next(i + 1);
				SwapElementsInArray(j, i);
			}
		}

		// Меняет местами два элемента в массиве
		private void SwapElementsInArray(int indexFirst, int indexSecond)
		{
			var FirstNumber = Array[indexFirst];
			var SecondNumber = Array[indexSecond];

			int temp = Array[indexFirst].Value;
			FirstNumber.Value = Array[indexSecond].Value;
			SecondNumber.Value = temp;
		}

		private void RestartSorting()
		{
			Low = 0;
			High = Array.Count - 1;
			ShuffleArray();
			UpdateInfo();
		}


	}
}

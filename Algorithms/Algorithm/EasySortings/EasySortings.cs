using System;

namespace Algorithms.Algorithm.EasySortings
{
	public class EasySortings : AlgorithmsProcessor
	{
		private string algorithmName = "Сортировка пузырьком";

		public int Low { get; private set; }
		public int High { get; private set; }


		public EasySortings()
		{
			AlgorithmEventHandler += BubbleSort;
			RestartEventHandler += RestartSortings;

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

		private void BubbleSort()
		{

		}

		private void BinSearch()
		{
			int selectedElement;
			while (!IsComplite)
			{
				if (!TimeManagement()) break;
				Attempt += 1;

				selectedElement = SelectedElement;
				//if (!GetArrayElement(mid, ref selectedElement)) return;
				SelectedElement = selectedElement;


			}
		}


		private void RestartSortings()
		{
			//Low = 0;
			//High = Array.Count - 1;
			UpdateInfo();
		}
	}
}

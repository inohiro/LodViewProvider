using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {

	public enum AggregationType {
		Average,
		Count,
		Max,
		Min
	}

	public class Aggregation : Condition {

		public string Variable { get; private set; }
		public AggregationType AggregationType { get; private set; }

		public Aggregation( string variable, AggregationType aggregationType ) {
			Variable = variable;
			AggregationType = aggregationType;
		}

		public override string ToString() {
			var strBuild = new StringBuilder();

			switch ( AggregationType ) {
				case LodViewProvider.AggregationType.Average: {

				} break;
				case LodViewProvider.AggregationType.Count: {

				} break;
				case LodViewProvider.AggregationType.Max: {

				} break;
				case LodViewProvider.AggregationType.Min: {

				} break;
				default: {
					throw new InvalidAggregationTypeException();
				} break;
			}

			return strBuild.ToString();
		}

	}

	public class InvalidAggregationTypeException : Exception {
	}
}

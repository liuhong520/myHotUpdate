﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	abstract class NativeCodeCreatorBase
	{
		public static string GetAS3RuntimeTypeString(Type type)
		{
			if (type.Equals(typeof(void)))
			{
				return "RunTimeDataType.fun_void";
			}
			else if (
				type.Equals(typeof(double))
				||
				type.Equals(typeof(float))
				)
			{
				//ASBinCode.RunTimeDataType.rt_number
				return "RunTimeDataType.rt_number";
			}
			else if (
				type.Equals(typeof(UInt16))
				||
				type.Equals(typeof(UInt32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_uint
				return "RunTimeDataType.rt_uint";
			}
			else if (
				type.Equals(typeof(Int16))
				||
				type.Equals(typeof(Int32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_int;
				return "RunTimeDataType.rt_int";
			}
			else if (
				type.Equals(typeof(Boolean))
				)
			{
				//ASBinCode.RunTimeDataType.rt_boolean;
				return "RunTimeDataType.rt_boolean";
			}
			else if (
				type.Equals(typeof(string))
				)
			{
				return "RunTimeDataType.rt_string";
			}

			return "RunTimeDataType.rt_void";
		}


		public static ASBinCode.RunTimeDataType GetAS3Runtimetype(Type type)
		{
			if (type.Equals(typeof(void)))
			{
				return ASBinCode.RunTimeDataType.fun_void;
			}
			else if (
				type.Equals(typeof(double))
				||
				type.Equals(typeof(float))
				)
			{
				//ASBinCode.RunTimeDataType.rt_number
				return ASBinCode.RunTimeDataType.rt_number;
			}
			else if (
				type.Equals(typeof(UInt16))
				||
				type.Equals(typeof(UInt32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_uint
				return ASBinCode.RunTimeDataType.rt_uint;
			}
			else if (
				type.Equals(typeof(Int16))
				||
				type.Equals(typeof(Int32))
				)
			{
				//ASBinCode.RunTimeDataType.rt_int;
				return ASBinCode.RunTimeDataType.rt_int;
			}
			else if (
				type.Equals(typeof(Boolean))
				)
			{
				//ASBinCode.RunTimeDataType.rt_boolean;
				return ASBinCode.RunTimeDataType.rt_boolean;
			}
			else if (
				type.Equals(typeof(string))
				)
			{
				return ASBinCode.RunTimeDataType.rt_string;
			}

			return ASBinCode.RunTimeDataType._OBJECT;
		}


		protected string GetLoadArgementString(Type parameterType,int position)
		{
			var rttype = GetAS3Runtimetype(parameterType);

			if (rttype > ASBinCode.RunTimeDataType.unknown)
			{
				if (parameterType.IsValueType && !parameterType.IsEnum)
				{
					string loadstructargement = Properties.Resources.LoadStructArgement;

					loadstructargement = loadstructargement.Replace("{argindex}", position.ToString());



					loadstructargement = loadstructargement.Replace("{argType}", parameterType.FullName);

					return loadstructargement;
				}
				else
				{
					string loadargement = Properties.Resources.LoadArgement;

					loadargement = loadargement.Replace("{argindex}", position.ToString());

					return loadargement;
				}
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_int)
			{
				return string.Format("\t\t\t\t\tint arg{0} = TypeConverter.ConvertToInt(argements[{0}]);", position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_uint)
			{
				return string.Format("\t\t\t\t\tuint arg{0} = TypeConverter.ConvertToUInt(argements[{0}], stackframe, token);", position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_number)
			{
				return string.Format("\t\t\t\t\tdouble arg{0} = TypeConverter.ConvertToNumber(argements[{0}]);", position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_boolean)
			{
				return string.Format("\t\t\t\t\tbool arg{0} = TypeConverter.ConvertToBoolean(argements[{0}], stackframe, token).value;", position);
			}
			else if (rttype == ASBinCode.RunTimeDataType.rt_string)
			{
				return string.Format("\t\t\t\t\tstring arg{0} = TypeConverter.ConvertToString(argements[{0}], stackframe, token);", position);
			}

			return "代码生成错误，不能转换参数类型";
		}

		protected string GetLoadArgementString(ParameterInfo paramenter)
		{
			return GetLoadArgementString(paramenter.ParameterType, paramenter.Position);
		}

	}
}
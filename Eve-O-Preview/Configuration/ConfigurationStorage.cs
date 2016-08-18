﻿using System.IO;
using Newtonsoft.Json;

namespace EveOPreview.Configuration
{
	public class ConfigurationStorage : IConfigurationStorage
	{
		private const string ConfigurationFileName = "EVE-O Preview.json";

		private readonly IAppConfig _configuration;

		public ConfigurationStorage(IAppConfig configuration)
		{
			this._configuration = configuration;
		}

		public void Load()
		{
			if (!File.Exists(ConfigurationStorage.ConfigurationFileName))
			{
				return;
			}

			string rawData = File.ReadAllText(ConfigurationStorage.ConfigurationFileName);

			JsonConvert.PopulateObject(rawData, this._configuration);

			// Validate data after loading it
			this._configuration.ApplyRestrictions();
		}

		public void Save()
		{
			string rawData = JsonConvert.SerializeObject(this._configuration, Formatting.Indented);

			File.WriteAllText(ConfigurationStorage.ConfigurationFileName, rawData);
		}
	}
}
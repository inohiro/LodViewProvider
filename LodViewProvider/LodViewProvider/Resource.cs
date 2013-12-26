using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

namespace LodViewProvider {

	public class Resource  { //: JObject {

		public Resource( string key ) {
			Key = key;
			Values = new Dictionary<string, string>();
		}

		public Resource( string key, Dictionary<string, string> values ) {
			Key = key;
			Values = values;
		}

		public string Key { get; private set; }
		public Dictionary<string, string> Values { get; private set; }
	}
}

/*
 * SPARQL Result JSON Format: http://www.w3.org/TR/sparql11-results-json/
 * 
 {
  "head": { "vars": [ "book" , "title" ]
  } ,
  "results": { 
	"bindings": [
	  {
		"book": { "type": "uri" , "value": "http://example.org/book/book6" } ,
		"title": { "type": "literal" , "value": "Harry Potter and the Half-Blood Prince" }
	  } ,
	  {
 */
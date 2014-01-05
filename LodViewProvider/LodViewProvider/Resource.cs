using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LodViewProvider {

	public class Resource  { //: JObject {

		public Dictionary<String, String> Values { get; set; }

		public Resource( string key ) {
			Values = new Dictionary<string, string>();
			Values.Add( key, "" );
		}

		public Resource( string key, string value ) {
			Values = new Dictionary<string, string>();
			Values.Add( key, value );
		}

		public Resource( Dictionary<String, String> values ) {
			Values = values;
		}

		public Resource() {
			Values = new Dictionary<string, string>();
		}

/*
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
*/
	}
}

/*
{ "head": { "link": [], "vars": ["subject"] },
  "results": { "distinct": false, "ordered": true, "bindings": [
	{ "subject": { "type": "uri", "value": "http://dbpedia.org/resource/Kita,_Tokyo" }},
	{ "subject": { "type": "uri", "value": "http://dbpedia.org/resource/Koto,_Tokyo" }},
	{ "subject": { "type": "uri", "value": "http://dbpedia.org/resource/Shinagawa,_Tokyo" }},
	{ "subject": { "type": "uri", "value": "http://dbpedia.org/resource/Sumida,_Tokyo" }},
	{ "subject": { "type": "uri", "value": "http://dbpedia.org/resource/Bunkyo,_Tokyo" }},
 */

/*
 * "results" [
 *   "subject": [ 'aaa', 'bbb', 'ccc' ],
 *   "hoge": [ 'aaa', 'bbb', 'ccc' ],
 * ]
 */

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
/*
* jQuery rStorage Plugin
*
* localStorage and sessionStorage helper utility for jQuery
* If you have idea about improving this plugin please let me know
*
* Copyright (c) 2014 rrd
*
* Licensed under the MIT license:
* http://opensource.org/licenses/MIT
*
* Project home:
* https://github.com/rrd108/rStorage
*
* Version: 1.0
*
*/
(function($){

	function RStorage(target){
		return {
			get : function(key){
				//returns part of the object identified by key (myNamespace.firstLevel.secondLevel) or return null if the key does not exists
				//TODO use only local not native
				key = key.split('.');
				var namespace = key.shift();	//get the first part like nsp from myNamespace.firstLevel.secondLevel				
				try{
					if (key.length) {		//still there are .-s in the key, walk into deeper
						var json = JSON.parse(target.getItem(namespace));
						for(var i = 0; i < key.length ; i++){
							json = json[key[i]];
						}
						return json;
					}
					else{
						return JSON.parse(target.getItem(namespace));
					}
				} catch(e){
					return target.getItem(namespace);
				}
			},

			_reset : function(key, json){
				if (key.search(/\./) == -1) {
					target.setItem(key, JSON.stringify(json));
					return json;
				}
				else{
					//the key is in dot notation form, reject it
					throw new Error(key + ' is an invalid key');
				}
			},

			remove : function(key){
				//removes part of the object identified by key (myNamespace.firstLevel.secondLevel)
				key = key.split('.');
				var namespace = key.shift();
				if (key.length) {		//3: canto2, stories, second
					var json = JSON.parse(target.getItem(namespace));
					var part = json[key[0]];
					//remove json.canto2.title or json['canto2']['title']
					for(var i = 1; i < (key.length -1) ; i++){	//we stop before the last, as that is we want to delete
						part = part[key[i]]
					}
					if (key[i]) {
						delete part[key[i]];
					}
					else{		//if there was only one dot we should directly remove from json
						delete json[key[0]];
					}
					this._reset(namespace, json);
				}
				else{
					target.removeItem(namespace);
				}
				return this.get(namespace);
			},

			set : function(key, json){
				//set part of the object identified by key (myNamespace.firstLevel.secondLevel) overwrites if it is already there, otherwise extends
				//TODO save to native and local also
				var originalJson = this.get(key);
				json = jQuery.extend(originalJson, json);
				return this._reset(key, json);
			}
		}
	}

	var rSessStorage = new RStorage(sessionStorage);
	$.sessionStorage = function(namespace, path){
		if (!path) {
			return rSessStorage.get(namespace);
		}
		else{
			return rSessStorage.set(namespace, path);
		}
	};
	$.sessionStorage.remove = function(namespace){
		return rSessStorage.remove(namespace);
	};
	
	var rLocStorage = new RStorage(localStorage);
	$.localStorage = function(namespace, path){
		if (!path) {
			return rLocStorage.get(namespace);
		}
		else{
			return rLocStorage.set(namespace, path);
		}
	};
	$.localStorage.remove = function(namespace){
		return rLocStorage.remove(namespace);
	};
	
}(jQuery));












/*


var sample = {
    widget: {
        debug: "on",
        window: {
            title: "The Title of The Widget",
            name: "widget_window",
            width: 500,
            height: 500
        },
        image: {
            src: "../images/goura.png",
            name: "Goura",
            hOffset: 250,
            vOffset: 250,
            alignment: "center"
        },
        text: {
            data: "Click Here",
            size: 36,
            style: "bold",
            name: "text1"
        }
    }
};

test('$.sessionStorage Test', function () {
    deepEqual($.sessionStorage('thisShouldBeNull'),
				 null,
				 'getter: non existent key returned null');

    deepEqual($.sessionStorage('testNamespace', { van: true }),
				 { van: true },
				 'setter: returned the correct object');

    deepEqual($.sessionStorage('testNamespace', { nincs: 5 }),
				 { van: true, nincs: 5 },
				 'setter: added new property and returned the new full object');

    deepEqual($.sessionStorage('testNamespace', { van: false, nincs: 5 }),
				 { van: false, nincs: 5 },
				 'setter: overwrote a property and returned the new full object when given unchanged property');

    deepEqual($.sessionStorage('testNamespace', { van: 13 }),
				 { van: 13, nincs: 5 },
				 'setter: overwrote a property and returned the new full object when given only what changed');

    deepEqual($.sessionStorage.remove('testNamespace.nincs'),
				 { van: 13 },
				 'remove: removed a sub-object and return the new object');


    deepEqual($.sessionStorage('testNamespace'),
				 { van: 13 },
				 'getter: gave the updated object after deleting one property');

    deepEqual($.sessionStorage.remove('testNamespace'),
				 null,
				 'remove: returned null after deleting the whole object');

    deepEqual($.sessionStorage('testNamespace', { a: 125 }),
				 { a: 125 },		//TODO this should not be a object, just simply true
				 'setter: returned the correct object');

    $.sessionStorage('sample', sample);

    deepEqual($.sessionStorage('sample.widget.debug'),
				 "on",
				 'getter: returned the proper sub-sub-object when called by dot notation');

    $.sessionStorage.remove('sample.widget.window');
    $.sessionStorage.remove('sample.widget.image');
    deepEqual($.sessionStorage.remove('sample.widget.text'),
				 { widget: { debug: 'on' } },
				 'remove: removed a sub-object and return the new object');
});

test('$.localStorage Test', function () {
    deepEqual($.localStorage('testNamespace'),
				 null,
				 'getter: returned null, as this namespace does not exists in $.localStorage, only in $.sessionStorage');

    deepEqual($.localStorage('testNamespace', { object: { level1: { level2: { level3: 'level3' } } } }),
				 { object: { level1: { level2: { level3: 'level3' } } } },
				 'setter: returned the new multi-level object');

    deepEqual($.localStorage('testNamespace.object'),
				 { level1: { level2: { level3: 'level3' } } },
				 'getter: returned the proper sub-object when called by dot notation');

    deepEqual($.localStorage('testNamespace.object.level1.level2'),
				 { level3: 'level3' },
				 'getter: returned the proper sub-sub-object when called by dot notation');

    deepEqual($.localStorage.remove('testNamespace'),
				 null,
				 'remove: removed the object and returned null');

    deepEqual($.localStorage('testNamespace'),
				 null,
				 'setter: returned null after the previous test deleted our object');
});

localStorage.clear();
sessionStorage.clear();

*/
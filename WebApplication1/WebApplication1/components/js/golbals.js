
$('.multi-dropdown a').on('click', function (event) {
    $(this).parent().toggleClass('open');
});

$('body').on('click', function (e) {
    if (!$('.multi-dropdown').is(e.target)
        && $('.multi-dropdown').has(e.target).length === 0
        && $('.open').has(e.target).length === 0
    ) {
        $('.multi-dropdown').removeClass('open');
    }
});


//GLOBAL FUNCTIONS AND VARIABLES

bURI = 'http://localhost';
bPorts = ["8080","8081"];
// bURI = '192.168.1.1';

REST = ':8080/api';
SCRAPELING = ':8081/api';

APP_PREFIX = 'hunt_';

AJAX_TIMEOUT = 100 * 300 * 1000;
ENV = 'dev';

var DEFAULT_DATE_FORMAT = 'YYYY-MM-DDTHH:mm:ss',
    DATE_FORMAT = 'DD.MM.YYYY';
var css;
csst = function(u){
  css = "";
  for(key in u){
   if(key!='cssText' && isNaN(key)){  
      console.log(key.toString(),u[key].toString()=="" ? 'none' : u[key].toString());
      var proper = u[key]=="" ? 'none' : u[key];
      css += key + ': ' + proper + ';';}
   }
}

Array.prototype.contains = function(elem)
{
 for (var i in this)
 {
   if (this[i] == elem) return true;
 }
 return false;
}

Array.prototype.remove = function(from, to) {
  var rest = this.slice((to || from) + 1 || this.length);
  this.length = from < 0 ? this.length + from : from;
  return this.push.apply(this, rest);
}

var API = {

    host: {
      webapp: bURI + ':' + bPorts[0] + "/api/",
      scrape: bURI + ':' + bPorts[1] + "/api/"
    },

    logout: function()
    {

    	API.storage.remove('isLoggedIn');
    	API.storage.remove('name');
    	API.storage.remove('role');
    	API.storage.set('remember',remember)

    },
    login: function(name,role,remember){
    	API.storage.set('name',name);
    	API.storage.set('role',role);
    	API.storage.set('isLoggedIn',true);
    	API.storage.set('remember',remember)
    },
    isLoggedIn: function()
    {
    	return API.storage.get('isLoggedIn');
    },
    capitaliseFirstLetter: function(string)
    {
    	return string.charAt(0).toUpperCase() + string.slice(1);
    },
    storage:
    {
	  get: function(key, skipParse)
	  {
	    var data = localStorage.getItem(APP_PREFIX + key);

	    if (data)
	    {
	      if (!skipParse)
	      {
	        data = JSON.parse(data);
	      }

	      return data;
	    }
	  },
      set: function(key, value, skipParse)
      {
        if (!skipParse)
        {
          value = JSON.stringify(value);
        }

        localStorage.setItem(APP_PREFIX + key, value);
      },
      remove: function(key)
      {
        localStorage.removeItem(APP_PREFIX + key);
      }
    },

    loader:
    {

      show: function(){
        $('body').addClass('loading');
        $('.loader-overlay').stop(true, true).show(0);
        $('.overlay').stop(true, true).show(0);

      },

      hide: function(){
        setTimeout(function(){
          $('body').removeClass('loading');
          $('.overlay').stop(true, true).hide(0);
          $('.loader-overlay').stop(true, true).hide(0);
        }, 1200);

      }

    }
  };
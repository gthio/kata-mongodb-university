var express = require('express'),
	app = express(),
	engines = require('consolidate'),
	nunjucks = require('nunjucks');

app.engine('html', engines.nunjucks);
app.set('view engine', 'html');
app.set('views', __dirname + '/views');

app.get('/', function(req, res){
	res.render('hello', {'name': 'Templates'});
});

app.use(function(req, res){
	res.status().send(404);
});

var server = app.listen(3000, function()
{
	var port = server.address().port;

	console.log('Express server listening on port %s', port);
});
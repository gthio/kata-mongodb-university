var express = require('express'),
	app = express(),
	engines = require('consolidate'),
	bodyParser = require('body-parser');

app.engine('html', engines.nunjucks);
app.set('view engine', 'html');
app.set('views', __dirname + '/views');
app.use(express.bodyParser());
app.use(app.router);

function errorHandler(err, req, res, next){
	console.error(err.message);
	console.error(err.stack);
	res.status(500);
	res.render('error_template', {error: err});
}

app.get('/', function(req, res, next){
	res.render('fruitPicker', {'fruits': ['apple', 'orange', 'banana', 'peach']});
});

app.post('/favorite_fruit', function(req, res, next){
	var favorite = req.body.fruit;

	if(typeof favorite == 'undefined'){
		next(Error('Please choose a fruit!'));
	}
	else{
		res.send("Your favorite fruit is " + favorite);
	}
});

app.use(errorHandler);

var server = app.listen(3000, function(){
	var port = server.address().port;
	console.log('Server listening on port %s.', port);
});
var casper = require('casper').create();

casper.start('http://mohundro.com', function() {
  this.test.assert(this.getCurrentUrl().indexOf('mohundro') !== -1, 'the url should not redirect away');
  this.test.assertTitle('David Mohundro', 'my blog has the title of my name');
});

casper.run(function() {
  this.test.renderResults(true);
});

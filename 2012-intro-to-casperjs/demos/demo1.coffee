casper = require('casper').create()

casper.start 'http://mohundro.com', () ->
  @test.assert @getCurrentUrl().indexOf('mohundro') != -1, 'the url should not redirect away'
  @test.assertTitle 'David Mohundro', 'my blog has the title of my name'

casper.run () ->
  @test.renderResults true

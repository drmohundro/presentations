# enabling verbose and/or logLevel can help with
# debugging tremendously

casper = require('casper').create
  verbose: true
  logLevel: 'debug'

casper.start 'http://www.amazon.com/', () ->
  # fills the form with the specified values (by name)
  # the third parameter of true means to submit the form
  @fill 'form[name="site-search"]',
    'field-keywords': 'Video Games'
  , true

casper.then () ->
  @test.assertExists '#breadCrumb', 'should have breadcrumb section'
  @test.assertEvalEquals () ->
    return document.querySelectorAll('#breadCrumb')[0].innerText
  , '"Video Games"', 'should have Video Games as the breadCrumb text'
  @test.assertTextExists 'Call of Duty', "surely they're trying to sell me Call of Duty... *sigh*"
  @test.assertTextExists 'Halo 4', "and isn't Halo 4 coming out?"

  @debugHTML()

casper.run () ->
  @test.renderResults true

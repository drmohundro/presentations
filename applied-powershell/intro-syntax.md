# Syntax Overview

<..>

## Variables

    # types aren't required...
    $aStringVar = 'hello there'
    # but they can be used
    [string]$anotherStr = 'hi again'
    $anotherStr = 5   # allowed because numbers can become strings

    [int]$intVar = 5
    $intVar = '55'
    $intVar = 'hello'   # error because this can't become a number

<aside class="notes" data-markdown>
  variables are prefixed with a $... feels like Perl huh?
</aside>

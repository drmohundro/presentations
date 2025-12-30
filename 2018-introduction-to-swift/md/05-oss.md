# OSS

<!-- .slide: data-background-image="https://user-images.githubusercontent.com/43072/39091331-9fc1cdc6-45b7-11e8-8e90-fdb782e3376e.gif" -->

<!-- .slide: class="shadowed-text" -->

<..>

## SOAP?!?

```objc
- (void)IsFeaturedEnabled:(NSString *)feature {
  SoapRequest *soapRequest = [[SoapRequest alloc] initWithUrl:@"/Soap/v2/Common.svc"];

  soapRequest.soapAction = @"http://redacted.com/v2/Common/ICommonApi/IsFeaturedEnabled";
  soapRequest.soapMessage = [NSString stringWithFormat:
    @"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n"
    "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://schemas.xmlsoap.org/soap/envelope/\">\n"
    "    <soap12:Header>\n"
    "        <AbSession xmlns=\"http://redacted.com/v2/\">\n"
    "            %@\n"
    "        </AbSession>\n"
    "    </soap12:Header>\n"
    "    <soap12:Body>\n"
    "        <IsFeaturedEnabled xmlns=\"http://redacted.com/v2/Common\">\n"
    "            <args>\n"
    "                <Feature>%@</Feature>\n"
    "            </args>\n"
    "        </IsFeaturedEnabled>\n"
    "    </soap12:Body>\n"
    "</soap12:Envelope>\n", [AbSoapApi abSession], feature];
  soapRequest.callback = @selector(IsFeaturedEnabledResult:);

  [self queueSoapRequest:soapRequest];
}
```

<..>

## SOAP?!?

```objc
- (void) parser:(NSXMLParser *)parser
didStartElement:(NSString *)elementName
   namespaceURI:(NSString *)namespaceURI
  qualifiedName:(NSString *)qName
     attributes:(NSDictionary *)attributeDict {
  //NSLog(@"found this element: %@", elementName);
  currentElement = [elementName copy];

  if ([elementName isEqualToString:@"a:Result"]) {
    // clear out our story item caches...
    currentString = [[[NSMutableString alloc] init] retain];
  }
}

- (void) parser:(NSXMLParser *)parser
foundCharacters:(NSString *)string {
  //NSLog(@"found characters: %@", string);
  // save the characters for the current item...
  if ([currentElement isEqualToString:@"a:Result"]) {
    [currentString appendString:string];
  }
}

- (void)parser:(NSXMLParser *)parser
 didEndElement:(NSString *)elementName
  namespaceURI:(NSString *)namespaceURI
 qualifiedName:(NSString *)qName {
  //NSLog(@"ended element: %@", elementName);
  if ([elementName isEqualToString:@"a:Result"]) {
    if ([currentString isEqualToString:@"false"]) {
      isEnabled = NO;
    } else {
      isEnabled = YES;
    }
    [currentString release];
  }
}
```

<..>

![Scream](https://user-images.githubusercontent.com/43072/39097728-f3b255fe-4625-11e8-973e-48c359f3ceda.gif)

<..>

## The Swift version wasn't much better...

```swift
func parser(
  parser: NSXMLParser!,
  didStartElement elementName: String!,
  namespaceURI: String!,
  qualifiedName: String!,
  attributes attributeDict: NSDictionary!) {
  self.parsingElement = elementName
  NSLog("Parsing \(elementName)")
}

func parser(
  parser: NSXMLParser!,
  foundCharacters string: String!) {
  var result = results[self.parsingElement]? ||| ""
  result += string
  results[self.parsingElement] = result
}

func parser(
  parser: NSXMLParser!,
  didEndElement elementName: String!,
  namespaceURI: String!,
  qualifiedName qName: String!) {
  if let result = results["a:Result"] {
    if result != "false" {
      self.isEnabled = true
    }
  }

  if let faultstring = results["faultstring"] {
    NSLog("ERROR \(faultstring)")
    self.errorMessage = faultstring
    //self.errorId = errorArray[1]
  }
}
```

<..>

## So I refactored it!

```swift
class IsFeatureEnabledResult : DeserializeFromXml {
  var errorMessage: String = ""
  var errorId: String = ""

  var isEnabled: Bool = false

  init() {}

  func deserialize(data: NSData) {
    NSLog("Received XML \(NSString(data: data, encoding:NSUTF8StringEncoding))")

    XmlParser(data: data, done: {results in
      if let result = results["a:Result"] {
        if result != "false" {
          self.isEnabled = true
        }
      }

      if let faultstring = results["faultstring"] {
        NSLog("ERROR \(faultstring)")
        self.errorMessage = faultstring
        //self.errorId = errorArray[1]
      }
    })
  }
}
```

<..>

## Then extracted it

![Refactored it out](https://user-images.githubusercontent.com/43072/39097918-94ee8b3e-4628-11e8-839e-61a45bc23633.png)

<..>

## Then pushed it to GitHub

![Initial commit](https://user-images.githubusercontent.com/43072/39097935-dfac8a36-4628-11e8-8728-fa77595ad2c1.png)

<..>

## SWXMLHash TODAY <!-- .element style="text-transform: none" -->

* [drmohundro/SWXMLHash](https://github.com/drmohundro/SWXMLHash/)
* Open source XML parsing library
* [Initially released on July 8, 2014](https://github.com/drmohundro/SWXMLHash/commit/497de2b)
* Over 900 stars
* [Used in over 14,000 apps](https://www.appsight.io/sdk/574840)

<..>

## Continuous Integration

* Travis support was added about a year later
  * (which is free for OSS!)

<..>

## What do you need to get started?

* GitHub
* Ideas! (or a desire to refactor/improve code)
* Willingness

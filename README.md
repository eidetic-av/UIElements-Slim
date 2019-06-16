# UIElements-Slim
Create Unity UIElements UXML files using the [Slim template engine](http://slim-lang.com/). Works great alongside [UIElements-Sass](https://github.com/eidetic-av/UIElements-Slim).

# Why
XML is a pain to write and looks messy. Slim removes the bloat and adds features like variables, functions, loops.

Here is iteration over an array of strings:
```slim
UXML
    Foldout class='column'
        - ['elephant', 'zebra', 'lion', 'giraffe'].each do |animal|
            - elementName = 'row-' << animal
            Box name=elementName
                - imagePath = 'Resources.Load(' << animal << 'Image)'
                Image image=imagePath class='animal-image'
                Label text=animal class='animal-label'
                - buttonText = 'Choose the ' << animal
                Button text=buttonText
```
It compiles to the following UXML:
```xml
<?xml version="1.0" encoding="utf-8"?>
<UXML xmlns:xsi="http:/www.w3.org/2001/XMLSchema-instance"
      xmlns="UnityEngine.UIElements"
      xsi:noNamespaceSchemaLocation="../UIElementsSchema/UIElements.xsd"
      xsi:schemaLocation="UnityEngine.UIElements ../UIElementsSchema/UnityEngine.UIElements.xsd">
  <Foldout class="column">
    <Box name="row-elephant">
      <Image class="animal-image" image="Resources.Load(elephantImage)"></Image>
      <Label class="animal-label" text="elephant"></Label>
      <Button text="Choose the elephant"></Button>
    </Box>
    <Box name="row-zebra">
      <Image class="animal-image" image="Resources.Load(zebraImage)"></Image>
      <Label class="animal-label" text="zebra"></Label>
      <Button text="Choose the zebra"></Button>
    </Box>
    <Box name="row-lion">
      <Image class="animal-image" image="Resources.Load(lionImage)"></Image>
      <Label class="animal-label" text="lion"></Label>
      <Button text="Choose the lion"></Button>
    </Box>
    <Box name="row-giraffe">
      <Image class="animal-image" image="Resources.Load(giraffeImage)"></Image>
      <Label class="animal-label" text="giraffe"></Label>
      <Button text="Choose the giraffe"></Button>
    </Box>
  </Foldout>
</UXML>
```
You can learn more about it's features [here](https://www.rubydoc.info/gems/slim/frames).

# Usage
This is just a proof-of-concept so your mileage may vary, but I've tested it on Windows and since it only consists of one script that calls to the command line, I don't see why it wouldn't work everywhere.

UIElements-Slim assumes you have a working ruby installation with the slim gem.
You can do this like so:

On Windows: `choco install ruby` then `gem install slim`.
Mac: `brew install ruby` then `gem install slim`.
Ubuntu: `sudo apt-get install ruby` then `gem install slim`.

Then you can clone this repo, add it as a submodule, or download the zip and extract it into your Unity project.

Every time a '.slim' file is added or updated, the script will run the slim compiler and output a '.uxml' file in the same directory.

# To-do
* It would be great if this didn't depend on a ruby installation and the command line...Unity uses nodejs for some of its processes so if I can figure out how to hook into this somehow perhaps the Slm project based on node can be used instead.

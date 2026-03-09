# Miside Zero Dialogue Manager

For use with [Miside Zero Dialogue Override](https://github.com/Gameknight963/Miside-Zero-Dialogue-Override)

## Building

1. Clone and build this repo.
2. Map the dialogue with [MSZDialogueMapper](https://github.com/Gameknight963/MSZDialogueMapper)
3. Put the mapped ``nodes.json`` in the build folder as ``templeteNodes.json``

## Usage

1. Click "Initialize Templete"
2. Add audio to a node by clicking "Select an audio file..."
3. When you're done, save the dialouge pack

You can also override the text and other properties of the node by pressing "edit properties.

**Note**: The ``Speaker`` property is used by  the game to determine which chirp to play for that dialogue clip.



**Other note**: 

It is not currently possible to provide a display on how long a given node will appear.



This is because the game's ``TypeText`` (IEnumerator in the DialogueManager class) has a bug making it FPS-dependent:

```csharp
private IEnumerator TypeText(string fullText, Text target)
{
	int num;
	for (int i = 0; i < fullText.Length; i = num + 1)
	{
		target.text += fullText[i].ToString();
		yield return new WaitForSeconds(this.typeSpeed);
		num = i;
	}
	yield break;
}
```

A fix looks something like this:

```csharp
private IEnumerator TypeText(string fullText, Text target)
{
    int i = 0;
    float elapsed = 0f;
    while (i < fullText.Length)
    {
        elapsed += Time.deltaTime;
        while (elapsed >= this.typeSpeed && i < fullText.Length)
        {
            target.text += fullText[i].ToString();
            elapsed -= this.typeSpeed;
            int num = i;
            i = num + 1;
        }
        yield return null;
    }
    yield break;
}
```

Kojo is aware of this bug but due to the current situation with the game I doubt it'll be fixed soon.



Information on how to use .mszdlg files can be found in the [Miside Zero Dialogue Override](https://github.com/Gameknight963/Miside-Zero-Dialogue-Override) repo

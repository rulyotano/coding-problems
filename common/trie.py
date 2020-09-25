class Trie:
    def __init__(self, character='', value=None):
        self.children = {}
        self.character = character
        self.value = value

    def add(self, stringValue, currentIndex=0):
        latestIndex = len(stringValue) - 1
        if currentIndex > latestIndex:
            return self

        character = stringValue[currentIndex]

        if not character in self.children:
            self.children[character] = Trie(character)

        return self.children[character].add(stringValue, currentIndex + 1)

    def find_node(self, searchString, currentIndex=0):
        latestIndex = len(searchString) - 1
        if currentIndex > latestIndex:
            return self

        character = searchString[currentIndex]

        if not character in self.children:
            return None

        return self.children[character].find_node(searchString, currentIndex + 1)

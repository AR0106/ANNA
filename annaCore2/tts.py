import sys
sys.path.insert(0, '/tts')

import tts


def annaSay(rate, volume, voice, saying):
    engine = tts.init() # object creation

    engine.setProperty('rate', rate)     # setting up new voice rate

    engine.setProperty('volume', volume)    # setting up volume level  between 0 and 1

    voices = engine.getProperty('voices')       #getting details of current voice
    engine.setProperty('voice', voices[voice].id)   #changing index, changes voices. 1 for female
    
    engine.say(saying)
    
    engine.runAndWait()
    engine.stop()
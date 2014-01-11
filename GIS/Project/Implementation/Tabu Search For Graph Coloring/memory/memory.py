class Memory:
    def __init__(self, short_term_memory_size):
        self.memory = []
        self.short_term_memory_size = short_term_memory_size

    def add_to_memory(self, node, color):
        self.memory.append((node.node_id, color))

    def clear_memory(self):
        self.memory = []

    def is_in_long_term_memory(self, node, color):
        return (node.node_id, color) in self.get_long_term_memory()

    def is_in_short_term_memory(self, node, color):
        return (node.node_id, color) in self.get_short_term_memory()

    def get_long_term_memory(self):
        return self.memory

    def get_short_term_memory(self):
        return self.memory[-self.short_term_memory_size:]

    def __str__(self):
        short_term_memory_as_string = ''

        for memory_entry in self.get_short_term_memory():
            short_term_memory_as_string += str(memory_entry[0]) + ' - ' + str(memory_entry[1]) + '\n'

        return short_term_memory_as_string

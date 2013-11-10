import unittest
from graph.node import Node
from memory.memory import Memory


class MemoryTests(unittest.TestCase):
    memory = Memory(3)

    def test_is_in_short_term_memory_method(self):
        self.memory.clear_memory()

        n1 = Node(1, 18)
        n2 = Node(2, 33)
        n3 = Node(3, 52)
        n4 = Node(4, 89)

        self.memory.add_to_memory(n1, 2)
        n1_2_in_memory_first = self.memory.is_in_short_term_memory(n1, 2)
        self.memory.add_to_memory(n2, 3)
        n1_2_in_memory_second = self.memory.is_in_short_term_memory(n1, 2)
        n2_3_in_memory_first = self.memory.is_in_short_term_memory(n2, 3)
        self.memory.add_to_memory(n3, 4)
        n1_2_in_memory_third = self.memory.is_in_short_term_memory(n1, 2)
        n2_3_in_memory_second = self.memory.is_in_short_term_memory(n2, 3)
        n3_4_in_memory_first = self.memory.is_in_short_term_memory(n3, 4)
        self.memory.add_to_memory(n4, 1)
        n1_2_in_memory_fourth = self.memory.is_in_short_term_memory(n1, 2)
        n2_3_in_memory_third = self.memory.is_in_short_term_memory(n2, 3)
        n3_4_in_memory_second = self.memory.is_in_short_term_memory(n3, 4)
        n4_1_in_memory_first = self.memory.is_in_short_term_memory(n4, 1)

        self.assertTrue(n1_2_in_memory_first)
        self.assertTrue(n1_2_in_memory_second)
        self.assertTrue(n2_3_in_memory_first)
        self.assertTrue(n1_2_in_memory_third)
        self.assertTrue(n2_3_in_memory_second)
        self.assertTrue(n3_4_in_memory_first)
        self.assertFalse(n1_2_in_memory_fourth)
        self.assertTrue(n2_3_in_memory_third)
        self.assertTrue(n3_4_in_memory_second)
        self.assertTrue(n4_1_in_memory_first)

    def test_is_in_long_term_memory_method(self):
        self.memory.clear_memory()

        n1 = Node(1)
        n2 = Node(2)
        n3 = Node(3)
        n4 = Node(4)

        self.memory.add_to_memory(n1, 2)
        n1_2_in_memory_first = self.memory.is_in_long_term_memory(n1, 2)
        self.memory.add_to_memory(n2, 3)
        n1_2_in_memory_second = self.memory.is_in_long_term_memory(n1, 2)
        n2_3_in_memory_first = self.memory.is_in_long_term_memory(n2, 3)
        self.memory.add_to_memory(n3, 4)
        n1_2_in_memory_third = self.memory.is_in_long_term_memory(n1, 2)
        n2_3_in_memory_second = self.memory.is_in_long_term_memory(n2, 3)
        n3_4_in_memory_first = self.memory.is_in_long_term_memory(n3, 4)
        self.memory.add_to_memory(n4, 1)
        n1_2_in_memory_fourth = self.memory.is_in_long_term_memory(n1, 2)
        n2_3_in_memory_third = self.memory.is_in_long_term_memory(n2, 3)
        n3_4_in_memory_second = self.memory.is_in_long_term_memory(n3, 4)
        n4_1_in_memory_first = self.memory.is_in_long_term_memory(n4, 1)

        self.assertTrue(n1_2_in_memory_first)
        self.assertTrue(n1_2_in_memory_second)
        self.assertTrue(n2_3_in_memory_first)
        self.assertTrue(n1_2_in_memory_third)
        self.assertTrue(n2_3_in_memory_second)
        self.assertTrue(n3_4_in_memory_first)
        self.assertTrue(n1_2_in_memory_fourth)
        self.assertTrue(n2_3_in_memory_third)
        self.assertTrue(n3_4_in_memory_second)
        self.assertTrue(n4_1_in_memory_first)

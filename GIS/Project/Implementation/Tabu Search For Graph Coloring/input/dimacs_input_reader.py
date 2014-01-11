import random
import re
from graph.node import Node


class DIMACSInputReader:
    def __init__(self):
        self.id_to_note_mapping = {}
        self.edge_ids = []
        self.color_set = None

    def read_input_graph_and_color_set(self, file_name):
        self.reset_input()

        with open(file_name, 'r') as graph_file:
            for line in graph_file:
                if len(line) > 0:
                    first_character = line.strip()[0]

                    if first_character == 'p':
                        match_result = re.match('p edge (\d+) (\d+)', line.strip())

                        if match_result is not None:
                            nodes = int(match_result.groups()[0])
                            self.color_set = range(0, nodes)
                        else:
                            raise IOError('Invalid input format!')

                    elif first_character == 'e':
                        match_result = re.match('e (\d+) (\d+)', line.strip())

                        if match_result is not None:
                            first_edge_id, second_edge_id = match_result.groups()[0], match_result.groups()[1]

                            if first_edge_id not in self.id_to_note_mapping:
                                self.id_to_note_mapping[first_edge_id] = Node(color=random.choice(self.color_set),
                                                                              node_id=first_edge_id)
                            if second_edge_id not in self.id_to_note_mapping:
                                self.id_to_note_mapping[second_edge_id] = Node(color=random.choice(self.color_set),
                                                                               node_id=second_edge_id)
                        else:
                            raise IOError('Invalid input format!')

                        self.edge_ids.append([first_edge_id, second_edge_id])

        if len(self.id_to_note_mapping) == 0:
            raise Exception('Invalid input format!')

        for edge in self.edge_ids:
            self.id_to_note_mapping[edge[0]].add_edges([self.id_to_note_mapping[edge[1]]])

        return self.id_to_note_mapping.items()[0][1], self.color_set

    def reset_input(self):
        self.id_to_note_mapping = {}
        self.edge_ids = []


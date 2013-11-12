import random
from graph.node import Node


class InputReader:
    def __init__(self):
        self.id_to_note_mapping = {}
        self.edge_ids = []
        self.color_classes_amount = None

    def input_graph(self, file_name):
        self.reset_input()

        with open(file_name, 'r') as graph_file:
            for line in graph_file:
                if self.color_classes_amount is None:
                    self.color_classes_amount = int(line.strip())

                    if self.color_classes_amount is None or self.color_classes_amount <= 0:
                        raise Exception('Invalid input format!')

                    continue

                edge = line.strip().split(',')

                if len(edge) != 2:
                    raise Exception('Invalid input format!')

                if edge[0] not in self.id_to_note_mapping:
                    self.id_to_note_mapping[edge[0]] = Node(color=random.choice(range(0, self.color_classes_amount)),
                                                            node_id=edge[0])

                if edge[1] not in self.id_to_note_mapping:
                    self.id_to_note_mapping[edge[1]] = Node(color=random.choice(range(0, self.color_classes_amount)),
                                                            node_id=edge[1])

                self.edge_ids.append(edge)

        if len(self.id_to_note_mapping) == 0:
            raise Exception('Invalid input format!')

        for edge in self.edge_ids:
            self.id_to_note_mapping[edge[0]].add_edges([self.id_to_note_mapping[edge[1]]])

        return self.id_to_note_mapping.items()[0][1]

    def reset_input(self):
        self.id_to_note_mapping = {}
        self.edge_ids = []

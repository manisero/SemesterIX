class NodeIterator:
    def __init__(self, root_node):
        self.root_node = root_node
        self.current_node = None
        self.visited_nodes = []

    def __iter__(self):
        return self

    def next(self):
        if self.current_node is None:
            self.current_node = self.root_node
        else:
            has_unvisited_child = False

            for child_node in self.current_node.edges:
                if child_node not in self.visited_nodes:
                    has_unvisited_child = True
                    self.current_node = child_node
                    break

            if not has_unvisited_child:
                self.current_node = self.recursive_search_next_node()

        if self.current_node is None:
            raise StopIteration

        self.visited_nodes.append(self.current_node)
        return self.current_node

    def recursive_search_next_node(self):
        current_node_index = self.visited_nodes.index(self.current_node)
        parent_found = False

        for index in range(current_node_index, -1, -1):
            previous_node = self.visited_nodes[index]

            if self.current_node in previous_node.edges:
                parent_found = True
                break

        if not parent_found:
            return None

        current_node_index_as_child = previous_node.edges.index(self.current_node)

        for index in range(current_node_index_as_child + 1, len(previous_node.edges), 1):
            if previous_node.edges[index] not in self.visited_nodes:
                return previous_node.edges[index]

        self.current_node = previous_node
        return self.recursive_search_next_node()

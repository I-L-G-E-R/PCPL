# RK1_refactored.py

from operator import itemgetter
from typing import List, Tuple, Dict

class Student:
    def __init__(self, id: int, fio: str, scholarship: float, group_id: int):
        self.id = id
        self.fio = fio
        self.scholarship = scholarship
        self.group_id = group_id

class Group:
    def __init__(self, id: int, name: str):
        self.id = id
        self.name = name

class StudentGroup:
    def __init__(self, student_id: int, group_id: int):
        self.student_id = student_id
        self.group_id = group_id


def get_one_to_many(students: List[Student], groups: List[Group]) -> List[Tuple[str, float, str]]:
    return [
        (s.fio, s.scholarship, g.name)
        for g in groups
        for s in students
        if s.group_id == g.id
    ]


def get_many_to_many(students: List[Student], groups: List[Group], students_groups: List[StudentGroup]) -> List[Tuple[str, float, str]]:
    many_to_many_temp = [
        (g.name, sg.student_id)
        for g in groups
        for sg in students_groups
        if g.id == sg.group_id
    ]
    return [
        (s.fio, s.scholarship, group_name)
        for group_name, student_id in many_to_many_temp
        for s in students
        if s.id == student_id
    ]


def task_d1(one_to_many: List[Tuple[str, float, str]]) -> List[Tuple[str, str]]:
    """Студенты с фамилией на 'ов' и их группы (один-ко-многим)"""
    return [(fio, group) for fio, _, group in one_to_many if fio.endswith('ов')]


def task_d2(one_to_many: List[Tuple[str, float, str]], groups: List[Group]) -> List[Tuple[str, float]]:
    """Средняя стипендия по группам, отсортировано по стипендии (один-ко-многим)"""
    result = []
    for g in groups:
        group_students = [sch for fio, sch, grp in one_to_many if grp == g.name]
        if group_students:
            avg = sum(group_students) / len(group_students)
            result.append((g.name, round(avg, 2)))
    return sorted(result, key=itemgetter(1))


def task_d3(many_to_many: List[Tuple[str, float, str]], groups: List[Group]) -> Dict[str, List[str]]:
    """Группы на 'А' и студенты в них (многие-ко-многим)"""
    result = {}
    for g in groups:
        if g.name.startswith('А'):
            students_in_group = [fio for fio, _, grp in many_to_many if grp == g.name]
            result[g.name] = students_in_group
    return result
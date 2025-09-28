use std::path::PathBuf;

use log::debug;

use crate::logic::exercises::repository::exercises::find_exercises_by_ids::EnrichedExerciseEntity;

pub fn render_latex(
    questions: Vec<EnrichedExerciseEntity>,
    mut filename_partial: PathBuf,
) -> anyhow::Result<()> {
    debug!("Latex rendering {:#?} items.", questions.len());
    let mut items: String = String::from("");
    questions.iter().for_each(|question| {
        let single_question = format!(
            r#"
            \newpage
            \paragraph{{Exercise}}\par
            \noindent
            \begin{{tabularx}}{{1\textwidth}}{{
                    p{{\dimexpr0.5\textwidth\relax}}
                    p{{\dimexpr0.5\textwidth\relax}}
                }}
                \toprule
                Book: & {book}
                \\
                \midrule
                Chapter: & {chapter}
                \\
                \midrule
                Section:: & {section_number} - {section_title}
                \\
                \midrule
                Page: & {page}
                \\
                \midrule
                Question type: & {question_type}
                \\
                \midrule
                Question number: & {question_number}
                \\
                \bottomrule
            \end{{tabularx}}
            \subparagraph{{Question}}
            {question_text}
            \subparagraph{{Solution}}
            {solution_text}
            "#,
            book = question.book_title,
            chapter = question.chapter_title,
            section_number = question.section_number,
            section_title = question.section_title,
            page = question.section_page_exercises_start,
            question_type = question.exercise_type,
            question_number = question.id_in_book,
            question_text = question.question,
            solution_text = question.solution
        );
        items.push_str(&single_question);
    });
    let latex = format!(
        r#"
        \documentclass{{article}}
        \usepackage{{tabularx}}
        \usepackage{{booktabs}}
        \usepackage{{amsmath}}
        \begin{{document}}
        {}
        \end{{document}}
        "#,
        items
    );

    filename_partial.set_extension("tex");
    match std::fs::write(&filename_partial, latex) {
        Ok(_) => {
            debug!("File written successfully: {:#?}", filename_partial.clone());
            Ok(())
        }
        Err(e) => {
            debug!("Error while writing the file: {:#?}", e);
            anyhow::bail!("Error while writing the file: {}", e);
        }
    }
}
